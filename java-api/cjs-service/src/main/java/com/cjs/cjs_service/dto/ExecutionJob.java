package com.cjs.cjs_service.dto;

import com.fasterxml.jackson.annotation.JsonInclude;

import lombok.Getter;
import lombok.Setter;

import java.util.UUID;

@Getter
@Setter
@JsonInclude(JsonInclude.Include.NON_NULL)
public class ExecutionJob {

    private String jobId = UUID.randomUUID().toString();

    private int submissionId = -1;

    private ExecutionStatus status = ExecutionStatus.PENDING;

    private CodeExecutionResult result;

    private String error;

    // ===============================
    // Getters & Setters
    // ===============================

}
