package com.cjs.cjs_service.dto;

import com.cjs.cjs_service.model.SubmissionStatus;
import com.fasterxml.jackson.annotation.JsonInclude;

import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
@JsonInclude(JsonInclude.Include.NON_NULL)
public class CodeExecutionResult {

    private String output = "";
    private String error = "";
    private int exitCode;
    private boolean timedOut;

    private SubmissionStatus submissionStatus = SubmissionStatus.PENDING;

    private List<TestCaseResultDto> testCaseResults = new ArrayList<>();


}

