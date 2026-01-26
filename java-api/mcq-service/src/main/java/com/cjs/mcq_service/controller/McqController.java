package com.cjs.mcq_service.controller;

import com.cjs.mcq_service.dto.request.CreateMcqDto;
import com.cjs.mcq_service.dto.request.SubmitMcqAnswerDto;
import com.cjs.mcq_service.dto.request.SaveMcqAttemptDto;
import com.cjs.mcq_service.dto.response.*;
import com.cjs.mcq_service.service.McqService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/mcq")
@RequiredArgsConstructor
public class McqController {
    
    private final McqService mcqService;
    
    @PostMapping
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<ApiResponseDto<McqQuestionWithAnswerDto>> createQuestion(
            @Valid @RequestBody CreateMcqDto dto,
            @RequestHeader("X-User-Id") Long userId) {
        try {
            McqQuestionWithAnswerDto question = mcqService.createQuestion(dto, userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "MCQ question created successfully", question));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping
    public ResponseEntity<ApiResponseDto<List<McqQuestionWithAnswerDto>>> getAllQuestions(
            @RequestParam(required = false) String category) {
        try {
            List<McqQuestionWithAnswerDto> questions = mcqService.getAllQuestions(category);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Questions retrieved successfully", questions));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping("/categories")
    public ResponseEntity<ApiResponseDto<List<String>>> getCategories() {
        try {
            List<String> categories = mcqService.getCategories();
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Categories retrieved successfully", categories));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<ApiResponseDto<McqQuestionWithAnswerDto>> getQuestion(@PathVariable Long id) {
        try {
            McqQuestionWithAnswerDto question = mcqService.getQuestionById(id);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Question retrieved successfully", question));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @PutMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<ApiResponseDto<McqQuestionWithAnswerDto>> updateQuestion(
            @PathVariable Long id, 
            @Valid @RequestBody CreateMcqDto dto) {
        try {
            McqQuestionWithAnswerDto question = mcqService.updateQuestion(id, dto);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Question updated successfully", question));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<ApiResponseDto<Boolean>> deleteQuestion(@PathVariable Long id) {
        try {
            boolean result = mcqService.deleteQuestion(id);
            String message = result ? "Question deleted successfully" : "Question not found";
            return ResponseEntity.ok(new ApiResponseDto<>(result, message, result));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @PostMapping("/quiz/start")
    public ResponseEntity<ApiResponseDto<QuizSessionDto>> startQuiz(
            @RequestParam(defaultValue = "10") Integer questionCount,
            @RequestParam(required = false) String category,
            @RequestParam(required = false) String difficulty,
            @RequestHeader("X-User-Id") Long userId) {
        try {
            QuizSessionDto session = mcqService.startQuiz(questionCount, category, difficulty, userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Quiz started successfully", session));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping("/quiz/{sessionId}/questions")
    public ResponseEntity<ApiResponseDto<List<McqQuestionDto>>> getQuizQuestions(@PathVariable Long sessionId) {
        try {
            List<McqQuestionDto> questions = mcqService.getQuizQuestions(sessionId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Quiz questions retrieved successfully", questions));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @PostMapping("/quiz/submit")
    public ResponseEntity<ApiResponseDto<McqAttemptResultDto>> submitAnswer(
            @Valid @RequestBody SubmitMcqAnswerDto dto,
            @RequestHeader("X-User-Id") Long userId) {
        try {
            McqAttemptResultDto result = mcqService.submitAnswer(dto, userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Answer submitted successfully", result));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping("/quiz/{sessionId}/result")
    public ResponseEntity<ApiResponseDto<QuizResultDto>> getQuizResult(
            @PathVariable Long sessionId,
            @RequestHeader("X-User-Id") Long userId) {
        try {
            QuizResultDto result = mcqService.getQuizResult(sessionId, userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Quiz result retrieved successfully", result));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @GetMapping("/quiz/history")
    public ResponseEntity<ApiResponseDto<List<QuizSessionDto>>> getQuizHistory(
            @RequestHeader("X-User-Id") Long userId) {
        try {
            List<QuizSessionDto> history = mcqService.getUserQuizHistory(userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Quiz history retrieved successfully", history));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage()));
        }
    }
    
    @PostMapping("/attempt")
    public ResponseEntity<ApiResponseDto<Boolean>> saveAttempt(
            @Valid @RequestBody SaveMcqAttemptDto dto,
            @RequestHeader("X-User-Id") Long userId) {
        try {
            mcqService.saveAttempt(dto, userId);
            return ResponseEntity.ok(new ApiResponseDto<>(true, "Attempt saved successfully", true));
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(new ApiResponseDto<>(false, e.getMessage(), false));
        }
    }
}