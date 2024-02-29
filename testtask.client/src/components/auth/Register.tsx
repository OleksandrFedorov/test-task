import { useContext, useEffect, useState } from "react";
import { Link, redirect } from "react-router-dom";
import { User, UserContext } from "../context/UserContext";
import GetUrl from "../context/UrlContext";

export default function Register() {
  {
    useEffect(() => {
      document.title = "Register";
    }, []);
  }

  const { user, setUser } = useContext(UserContext);
  const [values, setValues] = useState({
    name: "",
    password: "",
  });
  const [error, setError] = useState("");
  const [usedNames, setUsedName] = useState<string[]>([]);
  const url = GetUrl() + "Users";

  const handleInputChange = (e: any) => {
    e.preventDefault();

    const { name, value } = e.target;
    setValues((values) => ({
      ...values,
      [name]: value,
    }));
  };

  useEffect(() => {
    fetchUsedNames();
  }, []);

  const fetchUsedNames = async () => {
    fetch(url, {
      method: "GET",
    })
      .then(async (response) => {
        if (response.status === 200) {
          const text = await response.text();
          const usedNames: string[] = JSON.parse(text);
          setUsedName(usedNames);
        }
      })
      .catch((error) => setError(error));
  };

  const register = async () => {
    setUser({
      id: "",
      sessionId: "",
      name: values.name,
      password: values.password,
    });

    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("password", values.password);
    fetch(url, {
      method: "PUT",
      body: formData,
    })
      .then(async (response) => {
        if (response.status === 401) {
          setError("Invalid user name or password.");
          return;
        }
        const text = await response.text();
        const user: User = JSON.parse(text);

        setUser({
          id: user.id,
          sessionId: "",
          name: user.name,
          password: user.password,
        });

        redirect(GetUrl());
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

    if (usedNames.indexOf(values.name) > -1) {
      setError("This name is already taken");
      return;
    }

    register();
  };

  return (
    <>
      <div className="row d-flex justify-content-center mt-5">
        <div className="col-lg-3 text-center">
          {user.id && (
            <>
              <h2 className="title text-3xl font-bold m-3">
                Welcome {user.name}!
              </h2>
              <p className="mt-3">
                <Link to={"/login"}>Login</Link>
              </p>
            </>
          )}
          {!user.id && (
            <>
              <h2 className="title text-3xl font-bold m-3">Register</h2>
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
                  <button
                    className="btn btn-primary btn-lg"
                    onClick={handleSubmit}
                  >
                    Register
                  </button>
                </form>
                <p className="mt-3">
                  Already have an account? <Link to={"/login"}>Login</Link>
                </p>
              </div>
            </>
          )}
        </div>
      </div>
    </>
  );
}
