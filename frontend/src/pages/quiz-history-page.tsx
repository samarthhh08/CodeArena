import { useState, useEffect } from 'react';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import type { QuizSession } from '@/types/mcq';
import { mcqService } from '@/services/mcqService';
import { ArrowLeft } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

const QuizHistoryPage = () => {
  const navigate = useNavigate();
  const [sessions, setSessions] = useState<QuizSession[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadHistory();
  }, []);

  const loadHistory = async () => {
    try {
      setLoading(true);
      const historyData = await mcqService.getQuizHistory();
      setSessions(historyData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load quiz history');
    } finally {
      setLoading(false);
    }
  };

  const getScoreColor = (percentage: number) => {
    if (percentage >= 70) return 'text-green-600';
    if (percentage >= 50) return 'text-yellow-600';
    return 'text-red-600';
  };

  const getScoreBadge = (percentage: number) => {
    if (percentage >= 70) return 'bg-green-100 text-green-800';
    if (percentage >= 50) return 'bg-yellow-100 text-yellow-800';
    return 'bg-red-100 text-red-800';
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <div className="text-lg">Loading quiz history...</div>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="max-w-4xl mx-auto">
        <Button 
          onClick={() => navigate('/')} 
          variant="ghost" 
          className="mb-4 text-gray-600 hover:text-gray-900"
        >
          <ArrowLeft className="w-4 h-4 mr-2" />
          Back to Home
        </Button>
        <div className="flex justify-between items-center mb-8">
          <div>
            <h1 className="text-3xl font-bold text-gray-900 mb-2">Quiz History</h1>
            <p className="text-gray-600">Review your past quiz performances</p>
          </div>
          <Button onClick={() => window.location.href = '/mcq'}>
            Take New Quiz
          </Button>
        </div>

        {error && (
          <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded mb-6">
            {error}
          </div>
        )}

        {sessions.length === 0 ? (
          <Card>
            <CardContent className="text-center py-12">
              <div className="text-gray-500 mb-4">No quiz history found</div>
              <Button onClick={() => window.location.href = '/mcq'}>
                Take Your First Quiz
              </Button>
            </CardContent>
          </Card>
        ) : (
          <div className="space-y-4">
            {sessions.map((session) => (
              <Card key={session.id}>
                <CardContent className="p-6">
                  <div className="flex justify-between items-start">
                    <div className="flex-1">
                      <div className="flex items-center gap-4 mb-2">
                        <div className={`text-2xl font-bold ${getScoreColor(session.scorePercentage)}`}>
                          {session.scorePercentage.toFixed(1)}%
                        </div>
                        <div className={`px-3 py-1 rounded-full text-sm font-medium ${getScoreBadge(session.scorePercentage)}`}>
                          {session.scorePercentage >= 70 ? 'Excellent' : 
                           session.scorePercentage >= 50 ? 'Good' : 'Needs Improvement'}
                        </div>
                      </div>
                      
                      <div className="grid grid-cols-2 md:grid-cols-4 gap-4 text-sm text-gray-600 mb-3">
                        <div>
                          <span className="font-medium">Questions:</span> {session.totalQuestions}
                        </div>
                        <div>
                          <span className="font-medium text-green-600">Correct:</span> {session.correctAnswers}
                        </div>
                        <div>
                          <span className="font-medium text-red-600">Incorrect:</span> {session.incorrectAnswers}
                        </div>
                        <div>
                          <span className="font-medium">Status:</span> 
                          <span className={session.isCompleted ? 'text-green-600' : 'text-yellow-600'}>
                            {session.isCompleted ? ' Completed' : ' In Progress'}
                          </span>
                        </div>
                      </div>

                      <div className="text-sm text-gray-500">
                        <div>Started: {new Date(session.startedAt).toLocaleString()}</div>
                        {session.completedAt && (
                          <div>Completed: {new Date(session.completedAt).toLocaleString()}</div>
                        )}
                      </div>
                    </div>

                    <div className="flex flex-col gap-2">
                      {session.isCompleted && (
                        <Button
                          size="sm"
                          variant="outline"
                          onClick={() => window.location.href = `/mcq/results/${session.id}`}
                        >
                          View Details
                        </Button>
                      )}
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default QuizHistoryPage;