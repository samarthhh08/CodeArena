package com.cjs.mcq_service.model;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.AllArgsConstructor;

import java.time.LocalDateTime;
import java.util.List;

@Entity
@Table(name = "mcq_questions")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class McqQuestion {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @NotBlank
    @Column(columnDefinition = "TEXT")
    private String question;
    
    @NotBlank
    private String optionA;
    
    @NotBlank
    private String optionB;
    
    @NotBlank
    private String optionC;
    
    @NotBlank
    private String optionD;
    
    @NotBlank
    private String correctOption; // A, B, C, or D
    
    @NotBlank
    @Column(columnDefinition = "TEXT")
    private String correctExplanation;
    
    @Column(columnDefinition = "TEXT")
    private String incorrectExplanationA;
    
    @Column(columnDefinition = "TEXT")
    private String incorrectExplanationB;
    
    @Column(columnDefinition = "TEXT")
    private String incorrectExplanationC;
    
    @Column(columnDefinition = "TEXT")
    private String incorrectExplanationD;
    
    private String category = "General";
    
    private String difficulty = "Medium";
    
    @Column(name = "created_at")
    private LocalDateTime createdAt = LocalDateTime.now();
    
    @Column(name = "created_by")
    private Long createdBy;
    
    @OneToMany(mappedBy = "question", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<McqAttempt> attempts;
}