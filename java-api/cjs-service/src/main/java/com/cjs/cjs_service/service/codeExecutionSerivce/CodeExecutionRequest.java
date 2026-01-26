package com.cjs.cjs_service.service.codeExecutionSerivce;

import java.util.ArrayList;
import java.util.List;

import com.cjs.cjs_service.dto.TestCaseDto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CodeExecutionRequest {

    private String language = "";
    private String sourceCode = "";
    private String input; // nullable
    private int timeLimitMs = 2000;
    private int memoryLimitMb = 256;
    private List<TestCaseDto> testCases = new ArrayList<>();

    public CodeExecutionRequest(String language, String sourceCode, List<TestCaseDto> testCases) {
        this.language = language;
        this.sourceCode = sourceCode;
        this.testCases = testCases;
    }

}
