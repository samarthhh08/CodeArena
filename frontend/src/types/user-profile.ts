export type UserProfile = {
  id: string;
  username: string;
  email: string;
  about: string;
  avatarUrl: string;
  problemSolvingStats: {
    totalProblemsSolved: number;
    easyProblemsSolved: number;
    mediumProblemsSolved: number;
    hardProblemsSolved: number;
  };
};
