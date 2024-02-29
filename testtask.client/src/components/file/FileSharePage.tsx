import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FileShare } from "./File";
import FileCard from "./FileCard";
import GetUrl from "../context/UrlContext";

type Params = {
  id: string;
};

export default function FileSharePage() {
  const { id } = useParams<Params>();
  const url = GetUrl() + "files/shares/" + id;
  const [error, setError] = useState("");
  const [share, setFileShare] = useState<FileShare>();
  useEffect(() => {
    const fetchShare = async () => {
      fetch(url).then(async (response) => {
        if (!response.ok) {
          setError(await response.text());
        } else {
          const text = await response.text();
          const fileShare: FileShare = JSON.parse(text);
          setFileShare(fileShare);
        }
      });
    };

    fetchShare();
  }, []);
  {
    useEffect(() => {
      document.title = share?.file?.name ?? "Share";
    }, [share]);
  }

  return (
    <>
      <div className="container py-4 px-3 mx-auto">
        <h2 className="title text-3xl font-bold m-3">
          {error || !share
            ? error
            : "Shared intil " + new Date(share.expired).toUTCString()}
        </h2>

        {share && (
          <>
            <FileCard
              userId=""
              file={share.file!}
              showButtons={false}
              fullWidth={true}
            />
            <div className="d-flex justify-content-between mb-2"></div>
          </>
        )}
      </div>
    </>
  );
}
