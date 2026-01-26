package com.cjs.cjs_service.dto;

import com.cjs.cjs_service.model.SubmissionStatus;
import com.fasterxml.jackson.annotation.JsonInclude;

import lombok.Getter;
import lombok.Setter;


@Getter
@Setter
@JsonInclude(JsonInclude.Include.NON_NULL)
public class ProblemSubmissionDetails {

    private String title = "";

    private SubmissionStatus status = SubmissionStatus.PENDING;

    private String language = "";

   
}
