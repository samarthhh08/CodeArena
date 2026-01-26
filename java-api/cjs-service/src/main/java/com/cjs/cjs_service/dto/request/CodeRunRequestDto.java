package com.cjs.cjs_service.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class CodeRunRequestDto {

    private String code;
    private String language;
    private Integer problemId;
}
