import { useState } from 'react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Textarea } from '@/components/ui/textarea';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import type { CreateMcqDto } from '../../types/mcq';
import { mcqService } from '../../service/mcqService';

interface McqFormProps {
  onSuccess?: () => void;
  initialData?: CreateMcqDto;
  isEditing?: boolean;
  questionId?: number;
}

const McqForm = ({ onSuccess, initialData, isEditing = false, questionId }: McqFormProps) => {
  const [formData, setFormData] = useState<CreateMcqDto>(
    initialData || {
      question: '',
      optionA: '',
      optionB: '',
      optionC: '',
      optionD: '',
      correctOption: '',
      correctExplanation: '',
      incorrectExplanationA: '',
      incorrectExplanationB: '',
      incorrectExplanationC: '',
      incorrectExplanationD: '',
      category: 'Fixed',
      difficulty: 'Medium',
    }
  );
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      if (isEditing && questionId) {
        await mcqService.updateQuestion(questionId, formData);
      } else {
        await mcqService.createQuestion(formData);
      }
      
      if (onSuccess) onSuccess();
      
      if (!isEditing) {
        setFormData({
          question: '',
          optionA: '',
          optionB: '',
          optionC: '',
          optionD: '',
          correctOption: '',
          correctExplanation: '',
          incorrectExplanationA: '',
          incorrectExplanationB: '',
          incorrectExplanationC: '',
          incorrectExplanationD: '',
          category: 'Fixed',
          difficulty: 'Medium',
        });
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to save question');
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (field: keyof CreateMcqDto, value: string) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  return (
    <Card className="w-full max-w-4xl mx-auto">
      <CardHeader>
        <CardTitle>{isEditing ? 'Edit MCQ Question' : 'Add New MCQ Question'}</CardTitle>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-6">
          {error && (
            <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded">
              {error}
            </div>
          )}

          <div className="space-y-2">
            <Label htmlFor="question">Question *</Label>
            <Textarea
              id="question"
              value={formData.question}
              onChange={(e) => handleInputChange('question', e.target.value)}
              placeholder="Enter the MCQ question"
              required
              rows={3}
            />
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="optionA">Option A *</Label>
              <Input
                id="optionA"
                value={formData.optionA}
                onChange={(e) => handleInputChange('optionA', e.target.value)}
                placeholder="Option A"
                required
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="optionB">Option B *</Label>
              <Input
                id="optionB"
                value={formData.optionB}
                onChange={(e) => handleInputChange('optionB', e.target.value)}
                placeholder="Option B"
                required
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="optionC">Option C *</Label>
              <Input
                id="optionC"
                value={formData.optionC}
                onChange={(e) => handleInputChange('optionC', e.target.value)}
                placeholder="Option C"
                required
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="optionD">Option D *</Label>
              <Input
                id="optionD"
                value={formData.optionD}
                onChange={(e) => handleInputChange('optionD', e.target.value)}
                placeholder="Option D"
                required
              />
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div className="space-y-2">
              <Label htmlFor="correctOption">Correct Option *</Label>
              <Select value={formData.correctOption} onValueChange={(value) => handleInputChange('correctOption', value)}>
                <SelectTrigger>
                  <SelectValue placeholder="Select correct option" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="A">A</SelectItem>
                  <SelectItem value="B">B</SelectItem>
                  <SelectItem value="C">C</SelectItem>
                  <SelectItem value="D">D</SelectItem>
                </SelectContent>
              </Select>
            </div>
            <div className="space-y-2">
              <Label htmlFor="category">Category</Label>
              <Select value={formData.category} onValueChange={(value) => handleInputChange('category', value)}>
                <SelectTrigger>
                  <SelectValue placeholder="Select category" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="Fixed">Fixed</SelectItem>
                  <SelectItem value="Arrays">Arrays</SelectItem>
                  <SelectItem value="Strings">Strings</SelectItem>
                  <SelectItem value="Trees">Trees</SelectItem>
                  <SelectItem value="Graphs">Graphs</SelectItem>
                  <SelectItem value="Dynamic Programming">Dynamic Programming</SelectItem>
                  <SelectItem value="Sorting">Sorting</SelectItem>
                  <SelectItem value="Searching">Searching</SelectItem>
                  <SelectItem value="Linked Lists">Linked Lists</SelectItem>
                  <SelectItem value="Stacks and Queues">Stacks and Queues</SelectItem>
                  <SelectItem value="Hash Tables">Hash Tables</SelectItem>
                  <SelectItem value="Recursion">Recursion</SelectItem>
                </SelectContent>
              </Select>
            </div>
            <div className="space-y-2">
              <Label htmlFor="difficulty">Difficulty</Label>
              <Select value={formData.difficulty} onValueChange={(value) => handleInputChange('difficulty', value)}>
                <SelectTrigger>
                  <SelectValue placeholder="Select difficulty" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="Easy">Easy</SelectItem>
                  <SelectItem value="Medium">Medium</SelectItem>
                  <SelectItem value="Hard">Hard</SelectItem>
                </SelectContent>
              </Select>
            </div>
          </div>

          <div className="space-y-2">
            <Label htmlFor="correctExplanation">Correct Answer Explanation *</Label>
            <Textarea
              id="correctExplanation"
              value={formData.correctExplanation}
              onChange={(e) => handleInputChange('correctExplanation', e.target.value)}
              placeholder="Explain why this option is correct"
              required
              rows={3}
            />
          </div>

          <div className="space-y-4">
            <h3 className="text-lg font-medium">Incorrect Answer Explanations (Optional)</h3>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div className="space-y-2">
                <Label htmlFor="incorrectExplanationA">Why Option A is Wrong</Label>
                <Textarea
                  id="incorrectExplanationA"
                  value={formData.incorrectExplanationA || ''}
                  onChange={(e) => handleInputChange('incorrectExplanationA', e.target.value)}
                  placeholder="Explain why option A is incorrect"
                  rows={2}
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="incorrectExplanationB">Why Option B is Wrong</Label>
                <Textarea
                  id="incorrectExplanationB"
                  value={formData.incorrectExplanationB || ''}
                  onChange={(e) => handleInputChange('incorrectExplanationB', e.target.value)}
                  placeholder="Explain why option B is incorrect"
                  rows={2}
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="incorrectExplanationC">Why Option C is Wrong</Label>
                <Textarea
                  id="incorrectExplanationC"
                  value={formData.incorrectExplanationC || ''}
                  onChange={(e) => handleInputChange('incorrectExplanationC', e.target.value)}
                  placeholder="Explain why option C is incorrect"
                  rows={2}
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="incorrectExplanationD">Why Option D is Wrong</Label>
                <Textarea
                  id="incorrectExplanationD"
                  value={formData.incorrectExplanationD || ''}
                  onChange={(e) => handleInputChange('incorrectExplanationD', e.target.value)}
                  placeholder="Explain why option D is incorrect"
                  rows={2}
                />
              </div>
            </div>
          </div>

          <Button type="submit" disabled={loading} className="w-full">
            {loading ? 'Saving...' : isEditing ? 'Update Question' : 'Add Question'}
          </Button>
        </form>
      </CardContent>
    </Card>
  );
};

export default McqForm;