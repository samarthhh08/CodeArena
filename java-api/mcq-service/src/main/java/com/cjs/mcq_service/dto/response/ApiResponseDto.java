package com.cjs.mcq_service.dto.response;

import lombok.Data;

@Data
public class ApiResponseDto<T> {
    private Boolean success;
    private String message;
    private T data;
    
    public ApiResponseDto(Boolean success, String message) {
        this.success = success;
        this.message = message;
    }
    
    public ApiResponseDto(Boolean success, String message, T data) {
        this.success = success;
        this.message = message;
        this.data = data;
    }
}