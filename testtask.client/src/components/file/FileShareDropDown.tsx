import { FileSharesList } from "./FileSharesList";
import { FileShare } from "./File";
import FileShareAdd from "./FileShareAdd";

interface Props {
  fileId: string;
  shares: FileShare[];
}

export default function FileShareDropDown({ fileId, shares }: Props) {
  return (
    <>
      <div className="container py-1 px-0">
        <>
          <FileShareAdd fileId={fileId} />
          <FileSharesList shares={shares} />
        </>
      </div>
    </>
  );
}
