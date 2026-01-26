package com.cjs.mcq_service.dto.response;

import lombok.Data;
import java.util.List;

@Data
public class QuizResultDto {
    private QuizSessionDto session;
    private List<McqAttemptDetailDto> attempts;
}