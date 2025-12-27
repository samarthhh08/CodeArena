import { useEffect, useRef, useState } from "react";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import Editor from "@monaco-editor/react";
import { BOILERPLATE } from "./constant";
import axios, { AxiosError } from "axios";
import { Link } from "react-router-dom";
import { MaximizeIcon, MinimizeIcon } from "lucide-react";
import { Spinner } from "../ui/spinner";
import { FaCircleCheck, FaCircleUp, FaPlay } from "react-icons/fa6";
import { MdCancel } from "react-icons/md";
type Language = "node" | "java" | "cpp";

type ExecutionResult = {
  jobId: string;
  result: {
    exitCode: number;
    output: string;
    testCaseResults: { index: number; passed: boolean }[];
  };
};

type Props = {
  isAuthenticated: boolean;
  problemId: number;
};

const CodeEditor: React.FC<Props> = ({ isAuthenticated, problemId }) => {
  const [language, setLanguage] = useState<Language>("node");
  const [code, setCode] = useState(BOILERPLATE.node);
  const [actionDisable, setActionDisable] = useState(false);
  const [showRunSpinner, setShowRunSpinner] = useState(false);
  const [showSubmitSpinner, setShowSubmitSpinner] = useState(false);
  const [executionResult, setExecutionResult] =
    useState<ExecutionResult | null>(null);
  const [showResult, setShowResult] = useState(false);
  const [isMaximized, setIsMaximized] = useState(false);
  const [showProcessing,setShowProcessing] = useState(false);

  const pollingRef = useRef<number | null>(null);
  console.log(executionResult);

  useEffect(() => {
    return () => {
      if (pollingRef.current) clearInterval(pollingRef.current);
    };
  }, []);

  const handleRun = async () => {
    try {
      setActionDisable(true);
      setShowRunSpinner(true);
      setShowResult(false);
      setShowProcessing(true);

      const res = await axios.post(
        `http://localhost:5046/api/code/run`,
        { language, sourceCode: code, problemId: problemId },
        { withCredentials: true }
      );

      const jobId = res.data.data;

      pollingRef.current = setInterval(async () => {
        try {
          const res = await axios.get(
            `http://localhost:5046/api/code/status/${jobId}`,
            { withCredentials: true }
          );

          const status = res.data.data.status;

          if (status === "Completed" || status === "Failed") {
            clearInterval(pollingRef.current!);
            setExecutionResult(res.data.data);
            console.log(res.data.data);
            setShowResult(true);

            setIsMaximized(true);
            setActionDisable(false);
            setShowRunSpinner(false);
            setShowProcessing(false);
          }
        } catch {
          clearInterval(pollingRef.current!);
        }
      }, 1500);
    } catch (error) {
      setActionDisable(false);
      if (error instanceof AxiosError) {
        console.error(error.response?.data.message);
      }
    }
  };

  const handleSubmit = async () => {
    try {
      setShowProcessing(true);
      setActionDisable(true);
      setShowSubmitSpinner(true);
      setShowResult(false);

      const res = await axios.post(
        `http://localhost:5046/api/code/submit`,
        { language, sourceCode: code, problemId: problemId },
        { withCredentials: true }
      );

      const jobId = res.data.data;

      pollingRef.current = setInterval(async () => {
        try {
          const res = await axios.get(
            `http://localhost:5046/api/code/status/${jobId}`,
            { withCredentials: true }
          );

          const status = res.data.data.status;

          if (status === "Completed" || status === "Failed") {
            clearInterval(pollingRef.current!);
            setExecutionResult(res.data.data);
            console.log(res.data.data);
            setShowResult(true);

            setIsMaximized(true);
            setActionDisable(false);
            setShowSubmitSpinner(false);
            setShowProcessing(false);
          }
        } catch {
          clearInterval(pollingRef.current!);
        }
      }, 1500);
    } catch (error) {
      setActionDisable(false);
      if (error instanceof AxiosError) {
        console.error(error.response?.data.message);
      }
    }
  };

  return (
    <div className="w-full h-full min-h-full max-h-full flex flex-col border rounded-lg bg-background p-2 gap-y-2">
      {/* Header */}
      <div className="flex items-center justify-between border-b pb-1">
        <div className="w-48">
          <Select
            value={language}
            onValueChange={(lang) => {
              setLanguage(lang as Language);
              setCode(BOILERPLATE[lang]);
            }}
          >
            <SelectTrigger>
              <SelectValue placeholder="Select language" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="node">Node.js</SelectItem>
              <SelectItem value="cpp">C++</SelectItem>
              <SelectItem value="java">Java</SelectItem>
            </SelectContent>
          </Select>
        </div>
      </div>

      {/* Editor */}
      <div className="flex-1 overflow-hidden">
        <Editor
          height="100%"
          language={language === "node" ? "javascript" : language}
          value={code}
          onChange={(value) => setCode(value ?? "")}
          theme="vs-dark"
          options={{
            fontSize: 14,
            minimap: { enabled: false },
            automaticLayout: true,
            scrollBeyondLastLine: false,
            wordWrap: "on",
          }}
        />
      </div>

      {/* Actions */}
      {isAuthenticated && (
        <div className="flex gap-2">
          <Button
            variant="outline"
            onClick={handleRun}
            disabled={actionDisable}
            className=" bg-gray-200 text-xs w-24 h-7 px-4"
          >
            {showRunSpinner && <Spinner />}
            Run
            {!showRunSpinner && <FaPlay className="!w-[12px] !h-[12px]" />}
          </Button>
          <Button
            onClick={handleSubmit}
            variant={"outline"}
            className=" bg-gray-200 w-24 text-xs h-7 px-4"
            disabled={actionDisable}
          >
            {showSubmitSpinner && <Spinner />}
            Submit
            {!showSubmitSpinner && (
              <FaCircleUp className="!w-[12px] !h-[12px]" />
            )}
          </Button>
          {showProcessing && <p className="text-yellow-600 text-xs">Execution is in process...</p>}
        </div>
      )}

      {!isAuthenticated && (
        <div className="bg-gray-200 px-2 py-2 rounded">
          <p className="text-xs text-gray-700 font-semibold">
            You need to{" "}
            <Link className="text-blue-600" to="/signin">
              Log in
            </Link>{" "}
            to Run or Submit code
          </p>
        </div>
      )}

      {/* Result Panel */}
      <div
        className={`border rounded transition-all duration-300 overflow-hidden ${
          isMaximized ? "h-[50%]" : "h-12"
        }`}
      >
        <div className="flex items-center justify-between px-2 py-1 border-b">
          <p className="font-bold text-sm">Test Result</p>
          <Button
            variant="ghost"
            size="icon"
            onClick={() => setIsMaximized((v) => !v)}
          >
            {isMaximized ? (
              <MinimizeIcon size={16} />
            ) : (
              <MaximizeIcon size={16} />
            )}
          </Button>
        </div>

        <div className="p-2 overflow-y-auto h-full text-xs">
          {/* Runtime / Compilation Error */}
          {executionResult &&
            executionResult.result &&
            executionResult.result.exitCode !== 0 &&
            showResult && (
              <p className="text-red-400 whitespace-pre-wrap">
                {executionResult.result.output}
              </p>
            )}

          {executionResult && !executionResult.result && (
            <p className="text-red-400 whitespace-pre-wrap">
              "something went wrong"
            </p>
          )}
          {/* Test Case Results */}
          {executionResult &&
            executionResult.result &&
            executionResult.result.exitCode === 0 &&
            executionResult.result.testCaseResults.length > 0 &&
            showResult && (
              <div className="flex flex-wrap gap-2">
                {executionResult.result.testCaseResults.map((tc) => (
                  <div
                    key={tc.index}
                    className={` flex flex-row gap-x-2 justify-center items-center px-2 py-2 rounded bg-gray-200`}
                  >
                    {tc.passed ? (
                      <FaCircleCheck className="text-green-600 !w-[16px] !h-[16px] " />
                    ) : (
                      <MdCancel className="text-red-600 !w-[16px] !h-[16px]" />
                    )}{" "}
                    Test Case {`${tc.index}`}
                  </div>
                ))}
              </div>
            )}
        </div>
      </div>
    </div>
  );
};

export default CodeEditor;
