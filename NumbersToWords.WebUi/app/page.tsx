"use client";

import { useState } from "react";

type Result = {
  success: boolean;
  error: string | null;
  result: string | null;
};

export default function Home() {
  const [inputVal, setInputVal] = useState("");
  const [outputVal, setOutputVal] = useState("");

  const convertNumber = () => {
    if (inputVal.length > 0) {
      fetch(`/api/NumbersToWords?input=${inputVal}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      })
        .then(async (resp) => {
          const result: Result = await resp.json();
          if (result.success) {
            setOutputVal(result.result!);
          } else {
            setOutputVal(result.error!);
          }
        })
        .catch((err) => {
          setOutputVal(JSON.stringify(err));
        });
    } else {
      setOutputVal("No input provided.");
    }
  };

  return (
    <main className="flex min-h-screen flex-col items-center justify-center">
      <div className="grid gap-4">
        <div className="text-center">
          <p className="text-xl font-semibold border-b-2 border-solid border-blue-300">
            Numbers to Words
          </p>
        </div>

        <div className="text-left ml-4 max-w-80">
          <p className=" text-xs text-slate-500 ">Supported input format:</p>
          <p className="text-xs text-slate-500">
            <ul className="list-disc list-outside ml-6">
              <li>Must be a positive number, negative sign is not allowed</li>
              <li>
                Only allow number characters (1 to 9), except for one dot before
                Fractional part.
              </li>
              <li>Integer part: Up to 126 digits</li>
              <li>Fractional part: Up to 2 digits</li>
              <li>Sample input: 1234567.89</li>
            </ul>
          </p>
        </div>

        <div className="column-2 text-center">
          <input
            type="text"
            className="font-semibold p-3 rounded-l-md focus:outline-none focus:ring-1 focus:border-blue-300 shadow"
            value={inputVal}
            onChange={(e) => setInputVal(e.target.value)}
            onKeyUp={(e) => {
              if (e.key == "Enter") {
                convertNumber();
              }
            }}
          ></input>
          <button
            className="font-semibold bg-blue-300 hover:bg-blue-600 p-3 rounded-r-md shadow hover:text-slate-200"
            onClick={() => {
              convertNumber();
            }}
          >
            Convert
          </button>
        </div>
        {inputVal.length > 0 ? (
          <div className="text-center max-w-96">
            <div className="text-xs font-semibold p-3 rounded-lg shadow bg-blue-200">
              <p className="text-sm font-semibold border-b-2 border-solid border-slate-200 mb-4 pb-2">
                Input
              </p>
              <p className="break-all">{inputVal}</p>
            </div>
          </div>
        ) : (
          <></>
        )}
        {outputVal.length > 0 ? (
          <div className="text-center max-w-96">
            <div className="text-xs font-semibold p-3 rounded-lg shadow bg-blue-300">
              <p className="text-sm font-semibold border-b-2 border-solid border-slate-200 mb-4 pb-2">
                Output
              </p>
              <p>{outputVal}</p>
            </div>
          </div>
        ) : (
          <></>
        )}
      </div>
    </main>
  );
}
