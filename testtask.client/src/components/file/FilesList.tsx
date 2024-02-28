import { useEffect, useState } from "react";
import FileCard from "./FileCard";
import { File } from "../file/File";
import GetUrl from "../context/UrlContext";

interface Prpos {
  userId: string;
}

export function FilesList({ userId }: Prpos) {
  const [files, setFiles] = useState<File[]>([]);
  useEffect(() => {
    const dataFetch = async () => {
      const data = await (await fetch(GetUrl() + "files")).text();
      const files: File[] = JSON.parse(data);
      setFiles(files);
    };
    const millisecondsToWait = 400;
    setTimeout(function () {
      dataFetch();
    }, millisecondsToWait);
  });

  return (
    <>
      <div className="row m-1 align-items-start align-self-center justify-content-center">
        {files.map((item) => (
          <FileCard userId={userId} file={item} key={item.id + "card"} />
        ))}
      </div>
    </>
  );
}
