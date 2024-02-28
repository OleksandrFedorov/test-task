import Button from "./Button";
import { Navigate } from "react-router-dom";

interface Props {
  userName: string;
  logOut: () => void;
}

export default function Header({ userName, logOut }: Props) {
  return (
    <>
      {!userName && <Navigate to="/login" replace={true} />}
      <div className="container p-4 mx-auto">
        <div className="d-flex justify-content-end align-content-center">
          <h4 className="mx-2">{userName}</h4>
          <Button color="primary" isSmall={true} onClick={logOut}>
            Logout <i className="bi bi-door-open" />
          </Button>
        </div>
      </div>
    </>
  );
}
