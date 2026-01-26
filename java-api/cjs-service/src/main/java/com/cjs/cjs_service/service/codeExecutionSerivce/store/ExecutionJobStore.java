package com.cjs.cjs_service.service.codeExecutionSerivce.store;

import org.springframework.stereotype.Component;

import com.cjs.cjs_service.dto.ExecutionJob;

import java.util.concurrent.ConcurrentHashMap;

@Component
public class ExecutionJobStore {

    private final ConcurrentHashMap<String, ExecutionJob> jobs = new ConcurrentHashMap<>();

    public ExecutionJob createJob() {
        return createJob(-1);
    }

    public ExecutionJob createJob(int submissionId) {
        ExecutionJob job = new ExecutionJob();
        if (submissionId != -1) {
            job.setSubmissionId(submissionId);
        }
        jobs.put(job.getJobId(), job);
        return job;
    }

    public ExecutionJob getJob(String jobId) {
        return jobs.get(jobId);
    }
}
