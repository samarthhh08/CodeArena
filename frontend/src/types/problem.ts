export type Problem = {
  id:number,
  title: string;
  description: string;
  difficulty: "Easy" | "Medium" | "Hard";
  tags: string[];
  sampleTestCases: {
    input: string;
    output: string;
    explanation?: string;
  }[];
  constraints?: string[];
  hints?: string[];
};
