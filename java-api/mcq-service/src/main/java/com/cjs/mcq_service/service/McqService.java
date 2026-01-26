package com.cjs.mcq_service.service;

import com.cjs.mcq_service.dto.request.CreateMcqDto;
import com.cjs.mcq_service.dto.request.SubmitMcqAnswerDto;
import com.cjs.mcq_service.dto.request.SaveMcqAttemptDto;
import com.cjs.mcq_service.dto.response.*;

import java.util.List;

public interface McqService {
    
    McqQuestionWithAnswerDto createQuestion(CreateMcqDto dto, Long createdBy);
    
    List<McqQuestionWithAnswerDto> getAllQuestions(String category);
    
    List<String> getCategories();
    
    McqQuestionWithAnswerDto getQuestionById(Long id);
    
    McqQuestionWithAnswerDto updateQuestion(Long id, CreateMcqDto dto);
    
    boolean deleteQuestion(Long id);
    
    QuizSessionDto startQuiz(Integer questionCount, String category, String difficulty, Long userId);
    
    McqAttemptResultDto submitAnswer(SubmitMcqAnswerDto dto, Long userId);
    
    QuizResultDto getQuizResult(Long sessionId, Long userId);
    
    List<QuizSessionDto> getUserQuizHistory(Long userId);
    
    List<McqQuestionDto> getQuizQuestions(Long sessionId);
    
    void saveAttempt(SaveMcqAttemptDto dto, Long userId);
}