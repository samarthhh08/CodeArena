import type { Problem } from "@/types/problem";
import React from "react";
import { Card } from "../ui/card";
import { Link } from "react-router-dom";

type Props = {
  problems: Pick<Problem, "title" | "difficulty" | "id" | "slug">[];
};

const ProblemList: React.FC<Props> = ({ problems }) => {
  return (
    <Card className=" flex flex-col px-2 py-3 gap-y-2 w-full sm:max-w-[800px]">
      {problems.map((problem, i) => (
        <div className=" flex flex-col gap-y-2" key={problem.id}>
          <div className=" flex flex-row justify-between px-2 py-2">
            <Link
              to={`/problems/${problem.slug}/solution`}
              className="hover:cursor-pointer"
            >
              {problem.title}
            </Link>
            <p
              className={`uppercase text-xs font-bold ${
                problem.difficulty === "HARD"
                  ? " text-orange-600"
                  : problem.difficulty === "EASY"
                  ? "text-green-600"
                  : "text-yellow-600"
              }`}
            >
              {problem.difficulty}
            </p>
          </div>
          {i !== problems.length - 1 && (
            <div className="border-b border-gray-200" />
          )}
        </div>
      ))}
    </Card>
  );
};

export default ProblemList;
