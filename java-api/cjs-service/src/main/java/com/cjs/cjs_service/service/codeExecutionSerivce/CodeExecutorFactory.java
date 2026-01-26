package com.cjs.cjs_service.service.codeExecutionSerivce;




import org.springframework.stereotype.Component;

import java.util.List;

@Component
public class CodeExecutorFactory {

    private final List<CodeExecutorBase> executors;

    public CodeExecutorFactory(List<CodeExecutorBase> executors) {
        this.executors = executors;
    }

    public CodeExecutorBase getExecutor(String language) {
        return executors.stream()
                .filter(e -> e.getLanguage().equalsIgnoreCase(language))
                .findFirst()
                .orElseThrow(() ->
                        new UnsupportedOperationException(
                                "Language " + language + " not supported"));
    }
}
