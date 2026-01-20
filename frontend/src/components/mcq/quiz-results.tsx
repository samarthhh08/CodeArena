import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import type { QuizResult } from '../../types/mcq';
import { mcqService } from '../..//service/mcqService';

interface QuizResultsProps {
  sessionId: number;
  onStartNewQuiz: () => void;
}

const QuizResults = ({ sessionId, onStartNewQuiz }: QuizResultsProps) => {
  const [result, setResult] = useState<QuizResult | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadResults();
  }, [sessionId]);

  const loadResults = async () => {
    try {
      setLoading(true);
      const resultData = await mcqService.getQuizResult(sessionId);
      setResult(resultData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load results');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <div className="text-lg">Loading results...</div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded">
        {error}
      </div>
    );
  }

  if (!result) {
    return (
      <div className="text-center py-8">
        <div className="text-lg text-gray-600">No results found.</div>
      </div>
    );
  }

  const { session, attempts } = result;
  const scoreColor = session.scorePercentage >= 70 ? 'text-green-600' : 
                    session.scorePercentage >= 50 ? 'text-yellow-600' : 'text-red-600';

  return (
    <div className="max-w-4xl mx-auto space-y-6">
      {/* Overall Results */}
      <Card>
        <CardHeader>
          <CardTitle className="text-center">Quiz Results</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="text-center space-y-4">
            <div className={`text-6xl font-bold ${scoreColor}`}>
              {session.scorePercentage.toFixed(1)}%
            </div>
            <div className="text-lg text-gray-600">
              {session.correctAnswers} out of {session.totalQuestions} correct
            </div>
            <div className="flex justify-center gap-8 text-sm">
              <div className="text-green-600">
                ✓ Correct: {session.correctAnswers}
              </div>
              <div className="text-red-600">
                ✗ Incorrect: {session.incorrectAnswers}
              </div>
            </div>
            <div className="text-sm text-gray-500">
              Completed on {new Date(session.completedAt!).toLocaleString()}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Detailed Results */}
      <Card>
        <CardHeader>
          <CardTitle>Question by Question Review</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          {attempts.map((attempt, index) => (
            <div
              key={attempt.questionId}
              className={`p-4 border rounded-lg ${
                attempt.isCorrect ? 'border-green-200 bg-green-50' : 'border-red-200 bg-red-50'
              }`}
            >
              <div className="flex items-start gap-3 mb-3">
                <div className={`w-8 h-8 rounded-full flex items-center justify-center text-white font-medium ${
                  attempt.isCorrect ? 'bg-green-500' : 'bg-red-500'
                }`}>
                  {index + 1}
                </div>
                <div className="flex-1">
                  <div className="font-medium mb-2">{attempt.question}</div>
                  <div className="grid grid-cols-2 gap-2 text-sm">
                    <div>
                      <span className="text-gray-600">Your answer: </span>
                      <span className={attempt.isCorrect ? 'text-green-600' : 'text-red-600'}>
                        {attempt.selectedOption}
                      </span>
                    </div>
                    <div>
                      <span className="text-gray-600">Correct answer: </span>
                      <span className="text-green-600">{attempt.correctOption}</span>
                    </div>
                  </div>
                  <div className="mt-2 p-3 bg-white rounded border text-sm">
                    <div className="font-medium mb-1">Explanation:</div>
                    <div className="text-gray-700">{attempt.explanation}</div>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </CardContent>
      </Card>

      {/* Actions */}
      <div className="flex justify-center gap-4">
        <Button onClick={onStartNewQuiz} className="px-8">
          Take Another Quiz
        </Button>
        <Button variant="outline" onClick={() => window.location.href = '/mcq/history'}>
          View History
        </Button>
      </div>
    </div>
  );
};

export default QuizResults;