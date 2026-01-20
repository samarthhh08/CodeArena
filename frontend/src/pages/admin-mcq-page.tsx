import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import McqForm from '@/components/mcq/mcq-form';
import type { McqQuestionWithAnswer } from '../types/mcq';
import { mcqService } from '../service/mcqService';
import { FiEdit, FiTrash2, FiPlus } from 'react-icons/fi';
import { ArrowLeft } from 'lucide-react';
import { useNavigate } from 'react-router-dom';

const AdminMcqPage = () => {
  const navigate = useNavigate();
  const [questions, setQuestions] = useState<McqQuestionWithAnswer[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [showAddDialog, setShowAddDialog] = useState(false);
  const [editingQuestion, setEditingQuestion] = useState<McqQuestionWithAnswer | null>(null);
  const [showEditDialog, setShowEditDialog] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState<string>('All');
  const [categories, setCategories] = useState<string[]>([]);

  useEffect(() => {
    loadQuestions();
    loadCategories();
  }, []);

  useEffect(() => {
    loadQuestions();
  }, [selectedCategory]);

  const loadCategories = async () => {
    try {
      const categoriesData = await mcqService.getCategories();
      setCategories(['All', ...categoriesData]);
    } catch (err) {
      console.error('Failed to load categories:', err);
    }
  };

  const loadQuestions = async () => {
    try {
      setLoading(true);
      const category = selectedCategory === 'All' ? undefined : selectedCategory;
      const questionsData = await mcqService.getAllQuestions(category);
      setQuestions(questionsData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load questions');
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteQuestion = async (id: number) => {
    if (!confirm('Are you sure you want to delete this question?')) return;

    try {
      await mcqService.deleteQuestion(id);
      setQuestions(prev => prev.filter(q => q.id !== id));
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to delete question');
    }
  };

  const handleEditQuestion = (question: McqQuestionWithAnswer) => {
    setEditingQuestion(question);
    setShowEditDialog(true);
  };

  const handleFormSuccess = () => {
    setShowAddDialog(false);
    setShowEditDialog(false);
    setEditingQuestion(null);
    loadQuestions();
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <div className="text-lg">Loading questions...</div>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <Button 
        onClick={() => navigate('/admin')} 
        variant="ghost" 
        className="mb-4 text-gray-600 hover:text-gray-900"
      >
        <ArrowLeft className="w-4 h-4 mr-2" />
        Back to Admin Dashboard
      </Button>
      <div className="flex justify-between items-center mb-8">
        <div>
          <h1 className="text-3xl font-bold text-gray-900 mb-2">MCQ Management</h1>
          <p className="text-gray-600">Manage multiple choice questions for student practice</p>
        </div>
        
        <div className="flex items-center gap-4">
          {/* Category Filter */}
          <div className="flex items-center gap-2">
            <label className="text-sm font-medium text-gray-700">Category:</label>
            <select
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
              className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            >
              {categories.map((category) => (
                <option key={category} value={category}>
                  {category}
                </option>
              ))}
            </select>
          </div>

          <Dialog open={showAddDialog} onOpenChange={setShowAddDialog}>
            <DialogTrigger asChild>
              <Button className="flex items-center gap-2">
                <FiPlus className="w-4 h-4" />
                Add Question
              </Button>
            </DialogTrigger>
            <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
              <DialogHeader>
                <DialogTitle>Add New MCQ Question</DialogTitle>
              </DialogHeader>
              <McqForm onSuccess={handleFormSuccess} />
            </DialogContent>
          </Dialog>
        </div>
      </div>

      {error && (
        <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded mb-6">
          {error}
        </div>
      )}

      <div className="grid gap-6">
        {questions.length === 0 ? (
          <Card>
            <CardContent className="text-center py-12">
              <div className="text-gray-500 mb-4">No questions found</div>
              <Button onClick={() => setShowAddDialog(true)}>
                Add Your First Question
              </Button>
            </CardContent>
          </Card>
        ) : (
          questions.map((question) => (
            <Card key={question.id}>
              <CardHeader>
                <div className="flex justify-between items-start">
                  <div className="flex-1">
                    <CardTitle className="text-lg mb-2">{question.question}</CardTitle>
                    <div className="flex gap-2 text-sm">
                      <span className="bg-blue-100 px-2 py-1 rounded">{question.category}</span>
                      <span className="bg-green-100 px-2 py-1 rounded">{question.difficulty}</span>
                      <span className="bg-gray-100 px-2 py-1 rounded">
                        Correct: {question.correctOption}
                      </span>
                    </div>
                  </div>
                  <div className="flex gap-2">
                    <Button
                      size="sm"
                      variant="outline"
                      onClick={() => handleEditQuestion(question)}
                    >
                      <FiEdit className="w-4 h-4" />
                    </Button>
                    <Button
                      size="sm"
                      variant="outline"
                      onClick={() => handleDeleteQuestion(question.id)}
                      className="text-red-600 hover:text-red-700"
                    >
                      <FiTrash2 className="w-4 h-4" />
                    </Button>
                  </div>
                </div>
              </CardHeader>
              <CardContent>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3 mb-4">
                  {['A', 'B', 'C', 'D'].map((option) => (
                    <div
                      key={option}
                      className={`p-3 border rounded ${
                        question.correctOption === option
                          ? 'border-green-500 bg-green-50'
                          : 'border-gray-200'
                      }`}
                    >
                      <span className="font-medium">{option}:</span> {question[`option${option}` as keyof McqQuestionWithAnswer]}
                    </div>
                  ))}
                </div>
                <div className="p-3 bg-gray-50 rounded">
                  <div className="font-medium text-sm text-gray-700 mb-1">Explanation:</div>
                  <div className="text-sm text-gray-600">{question.correctExplanation}</div>
                </div>
              </CardContent>
            </Card>
          ))
        )}
      </div>

      {/* Edit Dialog */}
      <Dialog open={showEditDialog} onOpenChange={setShowEditDialog}>
        <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
          <DialogHeader>
            <DialogTitle>Edit MCQ Question</DialogTitle>
          </DialogHeader>
          {editingQuestion && (
            <McqForm
              initialData={{
                question: editingQuestion.question,
                optionA: editingQuestion.optionA,
                optionB: editingQuestion.optionB,
                optionC: editingQuestion.optionC,
                optionD: editingQuestion.optionD,
                correctOption: editingQuestion.correctOption,
                correctExplanation: editingQuestion.correctExplanation,
                incorrectExplanationA: editingQuestion.incorrectExplanationA,
                incorrectExplanationB: editingQuestion.incorrectExplanationB,
                incorrectExplanationC: editingQuestion.incorrectExplanationC,
                incorrectExplanationD: editingQuestion.incorrectExplanationD,
                category: editingQuestion.category,
                difficulty: editingQuestion.difficulty,
              }}
              isEditing={true}
              questionId={editingQuestion.id}
              onSuccess={handleFormSuccess}
            />
          )}
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default AdminMcqPage;