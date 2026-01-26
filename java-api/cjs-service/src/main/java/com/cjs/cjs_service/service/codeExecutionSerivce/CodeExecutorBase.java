package com.cjs.cjs_service.service.codeExecutionSerivce;

import com.cjs.cjs_service.dto.CodeExecutionResult;

public abstract class CodeExecutorBase {

    public abstract String getLanguage();

    public CodeExecutionResult execute(CodeExecutionRequest request) throws Exception {
        return executeInternal(request);
    }

    protected abstract CodeExecutionResult executeInternal(
            CodeExecutionRequest request) throws Exception;
}
