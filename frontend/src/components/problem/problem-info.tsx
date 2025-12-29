import type { Problem } from "@/types/problem";
import { Card } from "../ui/card";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";

type Props = {
  problem: Problem;
};

const ProblemInfo: React.FC<Props> = ({ problem }) => {
  return (
    <Card className="h-full flex flex-col px-4 sm:px-8 py-4">
      {/* Header */}
      <p className="font-bold text-xl sm:text-3xl mb-2">{problem.title}</p>

      <div className="flex flex-wrap gap-2 mb-4">
        {problem.tags.map((t, i) => (
          <span
            key={i}
            className="px-3 py-1 bg-gray-200 text-gray-700 rounded-full text-xs"
          >
            {t}
          </span>
        ))}
      </div>

      {/* Scrollable body */}
      <div className="flex-1 min-h-0 overflow-y-auto pr-2">
        <div className="prose prose-sm sm:prose-base max-w-none">
          <ReactMarkdown remarkPlugins={[remarkGfm]}>
            {problem.description}
          </ReactMarkdown>
        </div>

        <p className="font-bold text-lg mt-6">Sample Test Cases</p>

        <div className="flex flex-col gap-y-2 mt-2">
          {problem.sampleTestCases.map((s, i) => (
            <div
              key={i}
              className="bg-gray-200 px-3 py-2 rounded text-sm font-semibold"
            >
              <p>Input: {s.input}</p>
              <p>Output: {s.output}</p>
              {s.explanation && <p>Explanation: {s.explanation}</p>}
            </div>
          ))}
        </div>
      </div>
    </Card>
  );
};

export default ProblemInfo;
