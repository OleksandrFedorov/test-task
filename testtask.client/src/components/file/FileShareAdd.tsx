import { ChangeEvent, useState } from "react";
import Alert from "../Alert";
import GetUrl from "../context/UrlContext";

interface Props {
  fileId: string;
}

export default function FileShareAdd({ fileId }: Props) {
  const [error, setError] = useState("");
  const [days, setDays] = useState<number>(0);
  const [hours, setHours] = useState<number>(0);
  const [minutes, setMinutes] = useState<number>(0);
  function handleInputChange(
    e: ChangeEvent<HTMLInputElement>,
    set: (value: number) => void
  ) {
    e.preventDefault();
    const value =
      !Number.isNaN(e.target.valueAsNumber) && e.target.valueAsNumber > 0
        ? e.target.valueAsNumber
        : 0;
    set(value);
  }

  function add() {
    if (days > 0 || hours > 0 || minutes > 0) {
      const data = {
        fileId: fileId,
        days: days,
        hours: hours,
        minutes: minutes,
      };

      fetch(GetUrl() + "files/shares/", {
        method: "PUT",
        body: JSON.stringify(data),
        headers: new Headers({ "content-type": "application/json" }),
      });
    }
  }

  return (
    <>
      {error && <Alert onClose={() => setError("")}>{error}</Alert>}
      <div className="d-flex justify-content-between align-content-center">
        <form>
          <div className="d-flex justify-content-between mb-2">
            <div className="form-group col-md-3">
              <input
                className="form-control "
                type="number"
                value={days === 0 ? "" : days}
                placeholder="Days"
                onChange={(e) => handleInputChange(e, setDays)}
              />
            </div>
            <div className="form-group col-md-3">
              <input
                className="form-control "
                type="number"
                value={hours === 0 ? "" : hours}
                placeholder="Hours"
                onChange={(e) => handleInputChange(e, setHours)}
              />
            </div>
            <div className="form-group col-md-3">
              <input
                className="form-control "
                type="number"
                value={minutes === 0 ? "" : minutes}
                placeholder="Mins"
                onChange={(e) => handleInputChange(e, setMinutes)}
              />
            </div>
            <button
              className="btn btn-success btn-sm form-group col-md-2"
              type="button"
              onClick={add}
            >
              Add
            </button>
          </div>
        </form>
      </div>
    </>
  );
}
