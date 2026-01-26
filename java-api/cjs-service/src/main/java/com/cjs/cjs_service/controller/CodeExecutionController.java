package com.cjs.cjs_service.controller;

import com.cjs.cjs_service.dto.request.CodeRunRequestDto;
import com.cjs.cjs_service.dto.response.ApiResponseDto;
import com.cjs.cjs_service.model.Submission;
import com.cjs.cjs_service.service.SubmissionService;
import com.cjs.cjs_service.service.codeExecutionSerivce.CodeExecutionService;
import com.cjs.cjs_service.dto.ExecutionJob;

import org.springframework.http.ResponseEntity;

import org.springframework.security.core.Authentication;

import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/code")
public class CodeExecutionController {

    private final CodeExecutionService service;
    private final SubmissionService submissionService;

    public CodeExecutionController(
            CodeExecutionService service,
            SubmissionService submissionService) {
        this.service = service;
        this.submissionService = submissionService;
    }

    // ===============================
    // Run Code
    // ===============================
    // @PreAuthorize("isAuthenticated()")
    @PostMapping("/run")
    public ResponseEntity<ApiResponseDto<String>> runCode(
            @RequestBody CodeRunRequestDto dto) {

        try {
            String jobId = service.run(dto);

            return ResponseEntity.ok(
                    new ApiResponseDto<String>(
                            "Execution started",
                            jobId,
                            true

                    ));
        } catch (Exception e) {
            return ResponseEntity
                    .badRequest()
                    .body(new ApiResponseDto<>(
                            e.getMessage(),
                            null,
                            false));
        }
    }

    // ===============================
    // Submit Code
    // ===============================
    // @PreAuthorize("isAuthenticated()")
    @PostMapping("/submit")
    public ResponseEntity<ApiResponseDto<String>> submitCode(
            @RequestBody CodeRunRequestDto dto,
            Authentication authentication) {

        try {
            // Extract user id from JWT
            // Jwt jwt = (Jwt) authentication.getPrincipal();
            // Integer userId = jwt.getClaim("sub") != null
            // ? Integer.parseInt(jwt.getClaim("sub"))
            // : null;

            // if (userId == null) {
            // return ResponseEntity
            // .status(HttpStatus.UNAUTHORIZED)
            // .body(new ApiResponseDto<>(
            // "Invalid user id in token",
            // null,
            // false));
            // }

            Submission submission = submissionService.createSubmission(
                    1,
                    dto.getProblemId(),
                    dto.getCode(),
                    dto.getLanguage());

            String jobId = service.submit(dto, submission.getId());

            return ResponseEntity.ok(
                    new ApiResponseDto<>(
                            "Execution started",
                            jobId,
                            true

                    ));

        } catch (Exception e) {
            // ⚠️ Kept behavior same as your .NET code
            return ResponseEntity.ok(
                    new ApiResponseDto<>(
                            "Submission failed: " + e.getMessage(),
                            null,
                            false

                    ));
        }
    }

    // ===============================
    // Get Status
    // ===============================
    @GetMapping("/status/{jobId}")
    public ResponseEntity<ApiResponseDto<ExecutionJob>> getStatus(
            @PathVariable String jobId) {

        ExecutionJob job = service.getStatus(jobId);

        if (job == null) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(
                new ApiResponseDto<>(
                        "Status fetched",
                        job,
                        true

                ));
    }
}
