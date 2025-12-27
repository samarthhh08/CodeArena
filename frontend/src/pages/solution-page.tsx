import { useAuth } from "@/auth/useAuth";
import CodeEditor from "@/components/editor/code-editor";
import ProblemInfo from "@/components/problem/problem-info";

import axios, { AxiosError } from "axios";
import { useCallback, useEffect, useState } from "react";
// import { useParams } from "react-router-dom";

import type { Problem } from "@/types/problem";
const SolutionPage = () => {
  // const { id } = useParams();
  const { isAuthenticated, isLoading } = useAuth();
  const [isProblemLoading, setIsProblemLoading] = useState(true);
  const [problem, setProblem] = useState<Problem | null>(null);

  const fetchProblem = useCallback(async () => {
    try {
      // const res = await axios.get(`http://localhost:5046/api/problems/${id}`);
      const res = await axios.get(
        `http://localhost:5046/api/problems/reverse-string`
      );
      console.log(res.data.data);
      setProblem(res.data.data);
    } catch (error) {
      if (error instanceof AxiosError) {
        console.log(error.response?.data.message);
      }
    } finally {
      setIsProblemLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchProblem();
  }, [fetchProblem]);

  if (isLoading || isProblemLoading) return null;
  return (
    <div className="flex flex-col sm:flex-row w-full px-2 py-2 gap-x-2 gap-y-2 ">
      <div className="w-full sm:w-2/4 sm:h-[520px] lg:h-[620px] xl:[740px]">
        {problem && <ProblemInfo problem={problem} />}
      </div>
      <div className="w-full sm:w-2/4 sm:h-[520px] lg:h-[620px] xl:[740px]">
        {problem && (
          <CodeEditor
            isAuthenticated={isAuthenticated}
            problemId={problem.id}
          />
        )}
      </div>
    </div>
  );
};

export default SolutionPage;
