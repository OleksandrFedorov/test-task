import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
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
          if (new Date() > new Date(fileShare.expired)) {
            setError("Share is expiried :(");
          }
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

  //refresh page every 10 secons if expiried
  const navigate = useNavigate();
  useEffect(() => {
    const refreshPage = async () => {
      if (new Date() >= new Date(share?.expired ?? "")) {
        navigate(0);
      }
    };
    const millisecondsToWait = 10000;
    setTimeout(function () {
      refreshPage();
    }, millisecondsToWait);
  });

  return (
    <>
      <div className="container py-4 px-3 mx-auto">
        <h2 className="title text-3xl font-bold m-3">
          {error || !share
            ? error
            : "Shared until " + new Date(share.expired).toString()}
        </h2>

        {!error && share && (
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
