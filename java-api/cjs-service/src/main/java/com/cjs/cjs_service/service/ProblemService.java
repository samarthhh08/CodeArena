package com.cjs.cjs_service.service;

import com.cjs.cjs_service.dto.request.CreateProblemDto;
import com.cjs.cjs_service.dto.request.UpdateProblemDto;
import com.cjs.cjs_service.model.Problem;
import com.cjs.cjs_service.repository.ProblemRepository;
import com.cjs.cjs_service.service.ProblemService;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

@Service
@Transactional
public class ProblemService {

    private final ProblemRepository problemRepository;

    public ProblemService(ProblemRepository problemRepository) {
        this.problemRepository = problemRepository;
    }


    public Page<Problem> getProblems(
            String difficulty,
            String[] tags,
            Pageable pageable) {
        // For now: simple pagination
        // Later you can add Specification / filtering
        return problemRepository.findAll(pageable);
    }

 
    public Problem getProblemBySlug(String slug) {
        return problemRepository.findBySlug(slug).orElse(null);
    }

      public Problem getProblemById(Integer id) {
        return problemRepository.findById(id).orElse(null);
    }

    
    public int createProblem(CreateProblemDto dto) {
        Problem problem = new Problem();
        problem.setTitle(dto.getTitle());
        problem.setDescription(dto.getDescription());
        problem.setDifficulty(dto.getDifficulty());

        //generate slug here
        // problem.setSlug(dto.getSlug());

        Problem saved = problemRepository.save(problem);
        return saved.getId();
    }


    public boolean updateProblem(int id, UpdateProblemDto dto) {
        Optional<Problem> optionalProblem = problemRepository.findById(id);

        if (optionalProblem.isEmpty()) {
            return false;
        }

        Problem problem = optionalProblem.get();
        problem.setTitle(dto.getTitle());
        problem.setDescription(dto.getDescription());
        problem.setDifficulty(dto.getDifficulty());

        problemRepository.save(problem);
        return true;
    }
}
