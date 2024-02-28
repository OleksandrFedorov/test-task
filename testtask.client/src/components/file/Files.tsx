import { useContext, useEffect } from "react";
import { UserContext, defaultState } from "../context/UserContext";
import { FileUpload } from "./FileUpload";
import { FilesList } from "./FilesList";
import Header from "../Header";

export default function Files() {
  {
    useEffect(() => {
      document.title = "All Files";
    }, []);
  }

  const { user, setUser } = useContext(UserContext);
  const logOut = () => {
    setUser(defaultState.user);
  };

  return (
    <>
      <Header userName={user.name} logOut={logOut} />
      <div className="container px-1 mx-auto">
        <h1 className="title text-3xl font-bold m-3">Upload Files</h1>

        <FileUpload userId={user.id} />

        <h1 className="title text-3xl font-bold m-3">All Files</h1>
        <FilesList userId={user.id} />
      </div>
      ;
    </>
  );
}
