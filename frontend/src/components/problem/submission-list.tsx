import type { ProblemSubmission } from "@/types/problem";
import React from "react";

type Props = {
  submissions: ProblemSubmission[];
};

const SubmissionList: React.FC<Props> = ({ submissions }) => {
  return (
    <div className="text-sm text-gray-700">
      <p className="mb-3 font-medium">Your submissions for this problem</p>

      {submissions.length > 0 ? (
        <div className="overflow-x-auto">
          <table className="w-full border border-gray-200 rounded-md overflow-hidden">
            {/* Table Head */}
            <thead className="bg-gray-100 text-gray-600">
              <tr>
                {/* <th className="px-3 py-2 text-left font-medium">Title</th> */}
                <th className="px-3 py-2 text-left font-medium">Language</th>
                <th className="px-3 py-2 text-left font-medium">Status</th>
              </tr>
            </thead>

            {/* Table Body */}
            <tbody>
              {submissions.map((s, index) => (
                <tr
                  key={index}
                  className="border-t hover:bg-gray-50 transition"
                >
                  {/* Title (trimmed) */}
                  {/* <td className="px-3 py-2 max-w-[220px]">
                    <span
                      className="block truncate font-medium text-gray-800"
                      title={s.title}
                    >
                      {s.title}
                    </span>
                  </td> */}

                  {/* Language */}
                  <td className="px-3 py-2 text-gray-700">{s.language}</td>

                  {/* Status */}
                  <td className="px-3 py-2">
                    <span
                      className={`inline-block px-2 py-1 rounded text-xs font-semibold
                        ${
                          s.status === "ACCEPTED"
                            ? "bg-green-100 text-green-700"
                            : s.status === "WRONG_ANSWER"
                            ? "bg-red-100 text-red-700"
                            : "bg-yellow-100 text-yellow-700"
                        }`}
                    >
                      {s.status}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <p className="text-gray-500 text-sm">No submissions yet.</p>
      )}
    </div>
  );
};

export default SubmissionList;
