import { useState } from "react";
import Alert from "../Alert";
import { File } from "./File";
import GetUrl from "../context/UrlContext";
import FileShareDropDown from "./FileShareDropDown";

interface Prpos {
  userId: string;
  file: File;
  showButtons?: boolean;
  fullWidth?: boolean;
}

export default function FileCard({
  userId,
  file,
  showButtons = true,
  fullWidth = false,
}: Prpos) {
  const fileUrl = GetUrl() + "files/" + file.id;

  function getFileIcon(fileType: number) {
    switch (fileType) {
      case 1:
        return <i className="bi bi-filetype-pdf" />;
      case 2:
        return <i className="bi bi-filetype-xlsx" />;
      case 3:
        return <i className="bi bi-filetype-docx" />;
      case 4:
        return <i className="bi bi-filetype-txt" />;
      case 5:
        return <i className="bi bi-file-earmark-image" />;
      default:
        return <i className="bi bi-file-earmark" />;
    }
  }

  function onDownload() {
    fetch(fileUrl)
      .then((response) => response.blob())
      .then((blob) => {
        const url = window.URL.createObjectURL(new Blob([blob]));
        const link = document.createElement("a");
        link.href = url;
        link.download = file.name + "." + file.path.split(".").slice(-1);
        document.body.appendChild(link);

        link.click();

        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      })
      .catch((error) => {
        setError("Error on fetching the file: " + error);
      });
  }

  function onDelete() {
    fetch(fileUrl, {
      method: "DELETE",
    })
      .then((response) => {
        !response.ok && setError(response.statusText);
      })
      .catch((error) => {
        setError("Error on deliting the file: " + error);
      });
  }

  const [error, setError] = useState("");
  const [isShare, setIsShare] = useState(false);

  const created = new Date(file.created).toUTCString();
  const icon = getFileIcon(file.type);
  const thumbnail =
    file.type === 4 ? (
      <p className="fs-6 text-break">{file.thumbnail}</p>
    ) : file.thumbnail ? (
      <img
        src={`data:image/jpeg;base64,${file.thumbnail}`}
        style={{ maxWidth: "130px" }}
        className="border mx-auto"
      />
    ) : (
      <span className="fs-1">{icon}</span>
    );
  const downloadButton = (
    <button className="btn btn-success btn-sm" onClick={onDownload}>
      <i className="bi bi-file-earmark-arrow-down" /> Download
    </button>
  );
  const userButtons = (
    <>
      <button
        className="btn btn-warning btn-sm"
        onClick={() => setIsShare(!isShare)}
      >
        <i className="bi bi-share" /> Shares
      </button>
      <button
        className="btn btn-danger btn-sm"
        aria-label="Delete"
        onClick={onDelete}
      >
        <i className="bi bi-trash" />
      </button>
    </>
  );

  return (
    <>
      <div className="card m-2" style={{ width: fullWidth ? "100%" : "410px" }}>
        <div className="row g-0">
          <div
            className="col-md-4 my-2 d-flex align-items-center justify-content-center"
            style={{ maxWidth: "130px" }}
          >
            {thumbnail}
          </div>
          <div className="col-md-8">
            <div className="card-body">
              <h5 className="card-title text-break">
                {file.name} {icon}
              </h5>
              <p className="card-text">Created: {created}</p>
              {
                <div className="d-flex gap-1">
                  {downloadButton}
                  {showButtons && file.user.id === userId && userButtons}
                </div>
              }
              <p className="card-text">
                <small className="text-body-secondary">
                  Number of downloads: {file.numberOfDownloads}
                </small>
              </p>
              {error && <Alert onClose={() => setError("")}>{error}</Alert>}
            </div>
          </div>
        </div>
        {isShare && <FileShareDropDown fileId={file.id} shares={file.shares} />}
      </div>
    </>
  );
}
