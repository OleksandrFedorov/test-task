import Button from "../Button";
import GetUrl from "../context/UrlContext";
import { FileShare } from "../file/File";

interface Props {
  share: FileShare;
}

export function FileSharesListElement({ share }: Props) {
  const fileShareUrl = "https://localhost:5173/" + "shares/" + share.id;
  const expiredDate = new Date(share.expired);
  const expired = expiredDate.toUTCString();
  const className =
    expiredDate < new Date()
      ? "list-group-item list-group-item-warning d-flex justify-content-between align-items-center"
      : "list-group-item d-flex justify-content-between align-items-center";

  function onDeleteItem() {
    fetch(GetUrl() + "files/shares/" + share.id, {
      method: "DELETE",
    });
  }

  return (
    <>
      <li className={className} key={share.id}>
        <span>Until: {expired}</span>

        <div className="d-flex gap-1">
          <Button
            onClick={() => {
              navigator.clipboard.writeText(fileShareUrl);
            }}
            color="secondary"
            isSmall={true}
          >
            <i
              className="bi bi-clipboard2-plus"
              aria-label="Copy to Clipboard"
            />
          </Button>
          <Button onClick={onDeleteItem} color="danger" isSmall={true}>
            <i className="bi bi-trash" />
          </Button>
        </div>
      </li>
    </>
  );
}
