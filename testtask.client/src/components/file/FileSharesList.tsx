import { useId } from "react";
import { FileShare } from "../file/File";
import { FileSharesListElement } from "./FileSharesListElement";

interface Props {
  shares: FileShare[];
}

export function FileSharesList({ shares }: Props) {
  return (
    <>
      <ul className="list-group mb-1">
        {shares.map((item) => (
          <FileSharesListElement share={item} key={useId()} />
        ))}
      </ul>
    </>
  );
}
