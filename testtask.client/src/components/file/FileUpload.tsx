import { useCallback, useState } from "react";
import { FileRejection, useDropzone } from "react-dropzone";
import Button from "../Button";
import GetUrl from "../context/UrlContext";

interface Props {
  userId: string;
}

export function FileUpload({ userId: userId }: Props) {
  const [files, setFiles] = useState<File[]>([]);
  const [rejected, setRejected] = useState<FileRejection[]>([]);

  const onDrop = useCallback(
    (acceptedFiles: File[], rejectedFiles: FileRejection[]) => {
      if (acceptedFiles?.length) {
        setFiles((previousFiles) => [...previousFiles, ...acceptedFiles]);
      }

      if (rejectedFiles?.length) {
        setRejected((previousFiles) => [...previousFiles, ...rejectedFiles]);
      }
    },
    []
  );

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: {
      "application/pdf": [],
      "application/vnd.ms-excel": [],
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": [],
      "application/msword": [],
      "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
        [],
      "text/plain": [],
      "image/*": [],
    },
  });

  const removeFile = (input: File) => {
    setFiles((files) => files.filter((file) => file !== input));
  };

  const removeRejected = (input: File) => {
    setRejected((rejectedFiles) =>
      rejectedFiles.filter((rejected) => rejected.file !== input)
    );
  };

  const removeAll = () => {
    setFiles([]);
    setRejected([]);
  };

  const upload = async () => {
    if (files?.length) {
      files.forEach((file) => uploadFile(file));
    }
  };

  function uploadFile(file: File) {
    const url = GetUrl() + "files/" + userId;

    const formData = new FormData();
    formData.append("file", file);
    formData.append("name", file.name);
    formData.append("extention", file.type);
    formData.append("size", file.size.toString());

    fetch(url, {
      method: "PUT",
      body: formData,
    }).then((response) => response.ok && removeFile(file));
  }

  return (
    <>
      <div
        {...getRootProps({
          className: "dropzone card text-center  align-items-center m-3 pt-3",
        })}
      >
        <input {...getInputProps()} />
        {isDragActive ? (
          <p>Drop the files here ...</p>
        ) : (
          <p>Drag & drop files here, or click to select files</p>
        )}
      </div>
      <div className="px-3">
        {files.length > 0 && (
          <>
            <h2>Accepted Files</h2>
            <ul className="list-group">
              {files.map((file, index) => (
                <li
                  className="list-group-item d-flex gap-2 align-items-center"
                  key={"accepted_" + index}
                >
                  {file.name}
                  <button
                    className="btn btn-secondary btn-sm"
                    aria-label="Delete"
                    onClick={() => removeFile(file)}
                  >
                    Remove <i className="bi bi-trash" />
                  </button>
                </li>
              ))}
            </ul>
            <div className="d-flex gap-2 my-2">
              <Button color="primary" onClick={upload} isSmall={false}>
                Upload <i className="bi bi-file-earmark-arrow-up" />
              </Button>
              <Button color="danger" onClick={removeAll} isSmall={false}>
                Clear accepted <i className="bi bi-trash" />
              </Button>
            </div>
          </>
        )}
        {rejected.length > 0 && (
          <>
            <h2>Rejected Files</h2>
            <ul className="list-group">
              {rejected.map(({ file, errors }, index) => (
                <li
                  className="list-group-item list-group-item-danger d-flex gap-1 align-items-center"
                  key={file.name + index}
                >
                  <span>{file.name}</span>
                  {errors.map((error) => (
                    <span>
                      {error.code === "file-invalid-type"
                        ? "Invalid file type. File type must be PDF, XLS, XSLX, DOC, DOCX, TXT or any Image"
                        : +error.code + ": " + error.message}
                    </span>
                  ))}
                  <button
                    className="btn btn-secondary btn-sm ml-2"
                    aria-label="Delete"
                    onClick={() => removeRejected(file)}
                  >
                    Remove <i className="bi bi-trash" />
                  </button>
                </li>
              ))}
            </ul>
            <div className="my-2">
              <Button color="danger" onClick={removeAll} isSmall={false}>
                {"Clear all"} <i className="bi bi-trash" />
              </Button>
            </div>
          </>
        )}
      </div>
    </>
  );
}
