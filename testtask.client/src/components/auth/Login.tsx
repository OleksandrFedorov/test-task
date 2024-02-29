import { useContext, useEffect, useState } from "react";
import { Link, Navigate } from "react-router-dom";
import { UserContext, UserSession } from "../context/UserContext";
import GetUrl from "../context/UrlContext";

export default function Login() {
  {
    useEffect(() => {
      document.title = "Login";
    }, []);
  }

  const { user, setUser } = useContext(UserContext);
  const [values, setValues] = useState({
    name: "",
    password: "",
  });
  const [error, setError] = useState("");

  const handleInputChange = (e: any) => {
    e.preventDefault();

    const { name, value } = e.target;
    setValues((values) => ({
      ...values,
      [name]: value,
    }));
  };

  const login = async () => {
    setUser({
      id: "",
      sessionId: "",
      name: values.name,
      password: values.password,
    });

    const url = GetUrl() + "Users/login";

    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("password", values.password);
    fetch(url, {
      method: "POST",
      body: formData,
    })
      .then(async (response) => {
        if (response.status === 401) {
          setError("Invalid user name or password.");
          return;
        }
        const text = await response.text();
        const session: UserSession = JSON.parse(text);

        setUser({
          id: session.user.id,
          sessionId: session.id,
          name: session.user.name,
          password: session.user.password,
        });
      })
      .catch((error) => setError(error));
  };

  const handleSubmit = (e: any) => {
    e.preventDefault();

    setError("");
    if (!values.name) {
      setError("Please enter a name");
      return;
    }

    if (!values.password) {
      setError("Please enter a password");
      return;
    }

    login();
  };

  return (
    <>
      {user.sessionId && <Navigate to="/" replace={true} />}

      <div className="row d-flex justify-content-center mt-5">
        <div className="col-lg-3 text-center">
          <h2 className="title text-3xl font-bold m-3">Login</h2>
          <div className="form-group">
            <form className="">
              <input
                className="form-control m-3"
                type="text"
                placeholder="Name"
                name="name"
                value={values.name}
                onChange={handleInputChange}
              />
              {error && (
                <p id="error" className="text-danger">
                  {error}
                </p>
              )}
              <input
                className="form-control m-3"
                type="password"
                placeholder="Password"
                name="password"
                value={values.password}
                onChange={handleInputChange}
              />
              <button className="btn btn-primary btn-lg" onClick={handleSubmit}>
                Login
              </button>
            </form>
            <p className="mt-3">
              Don't have an account? <Link to={"/register"}>Register</Link>
            </p>
          </div>
        </div>
      </div>
    </>
  );
}
