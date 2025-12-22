import type { Problem } from "@/types/problem";

export const problems: Problem[] = [
  {
    title: "Two Sum",
    description:
      "Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.",
    difficulty: "Easy",
    tags: ["Array", "Hash Table"],
    sampleTestCases: [
      {
        input: "nums = [2,7,11,15], target = 9",
        output: "[0,1]",
        explanation: "nums[0] + nums[1] = 2 + 7 = 9",
      },
    ],
    constraints: ["2 <= nums.length <= 10^4"],
    hints: ["Use a hash map to store visited numbers"],
  },
  {
    title: "Reverse String",
    description: "Write a function that reverses a string.",
    difficulty: "Easy",
    tags: ["String"],
    sampleTestCases: [
      {
        input: 's = "hello"',
        output: '"olleh"',
      },
    ],
    constraints: ["1 <= s.length <= 10^5"],
  },
  {
    title: "Valid Parentheses",
    description:
      "Given a string containing brackets, determine if the input string is valid.",
    difficulty: "Easy",
    tags: ["Stack", "String"],
    sampleTestCases: [
      {
        input: 's = "()[]{}"',
        output: "true",
      },
    ],
    hints: ["Use a stack"],
  },
  {
    title: "Merge Two Sorted Lists",
    description:
      "Merge two sorted linked lists and return it as a sorted list.",
    difficulty: "Medium",
    tags: ["Linked List"],
    sampleTestCases: [
      {
        input: "l1 = [1,2,4], l2 = [1,3,4]",
        output: "[1,1,2,3,4,4]",
      },
    ],
  },
  {
    title: "Longest Substring Without Repeating Characters",
    description:
      "Find the length of the longest substring without repeating characters.",
    difficulty: "Medium",
    tags: ["String", "Sliding Window"],
    sampleTestCases: [
      {
        input: 's = "abcabcbb"',
        output: "3",
        explanation: 'The answer is "abc"',
      },
    ],
  },
  {
    title: "Search in Rotated Sorted Array",
    description: "Search for a target value in a rotated sorted array.",
    difficulty: "Medium",
    tags: ["Array", "Binary Search"],
    sampleTestCases: [
      {
        input: "nums = [4,5,6,7,0,1,2], target = 0",
        output: "4",
      },
    ],
  },
  {
    title: "Climbing Stairs",
    description:
      "You can climb either 1 or 2 steps. In how many distinct ways can you reach the top?",
    difficulty: "Medium",
    tags: ["Dynamic Programming"],
    sampleTestCases: [
      {
        input: "n = 3",
        output: "3",
      },
    ],
  },
  {
    title: "Maximum Subarray",
    description: "Find the contiguous subarray which has the largest sum.",
    difficulty: "Hard",
    tags: ["Array", "Dynamic Programming"],
    sampleTestCases: [
      {
        input: "nums = [-2,1,-3,4,-1,2,1,-5,4]",
        output: "6",
        explanation: "Subarray [4,-1,2,1]",
      },
    ],
  },
  {
    title: "Serialize and Deserialize Binary Tree",
    description:
      "Design an algorithm to serialize and deserialize a binary tree.",
    difficulty: "Hard",
    tags: ["Tree", "DFS", "BFS"],
    sampleTestCases: [
      {
        input: "root = [1,2,3,null,null,4,5]",
        output: "[1,2,3,null,null,4,5]",
      },
    ],
  },
  {
    title: "Binary Tree Inorder Traversal",
    description: "Return the inorder traversal of a binary tree.",
    difficulty: "Easy",
    tags: ["Tree", "DFS"],
    sampleTestCases: [
      {
        input: "root = [1,null,2,3]",
        output: "[1,3,2]",
      },
    ],
  },
  {
    title: "Fibonacci Number",
    description: "Calculate the nth Fibonacci number.",
    difficulty: "Easy",
    tags: ["Recursion", "Dynamic Programming"],
    sampleTestCases: [
      {
        input: "n = 4",
        output: "3",
      },
    ],
  },
  {
    title: "Graph Valid Tree",
    description: "Determine if the given edges form a valid tree.",
    difficulty: "Hard",
    tags: ["Graph", "Union Find"],
    sampleTestCases: [
      {
        input: "n = 5, edges = [[0,1],[0,2],[0,3],[1,4]]",
        output: "true",
      },
    ],
  },
];
