package com.cjs.cjs_service.dto;



import com.fasterxml.jackson.annotation.JsonInclude;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@JsonInclude(JsonInclude.Include.NON_NULL)
public class TestCaseResultDto {

    private int index;
    private String input = "";
    private String output = "";
    private String expected = "";
    private boolean passed;

}

