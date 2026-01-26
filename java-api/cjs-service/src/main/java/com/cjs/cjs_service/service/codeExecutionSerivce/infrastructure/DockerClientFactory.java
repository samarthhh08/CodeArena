package com.cjs.cjs_service.service.codeExecutionSerivce.infrastructure;



import com.github.dockerjava.api.DockerClient;
import com.github.dockerjava.core.DefaultDockerClientConfig;
import com.github.dockerjava.core.DockerClientImpl;
import com.github.dockerjava.httpclient5.ApacheDockerHttpClient;

import java.time.Duration;

public final class DockerClientFactory {

    private DockerClientFactory() {}

    public static DockerClient create() {

        DefaultDockerClientConfig config =
                DefaultDockerClientConfig.createDefaultConfigBuilder()
                        // LOCAL (default)
                        // uses unix:///var/run/docker.sock (Linux)
                        // or npipe://./pipe/docker_engine (Windows)
                        .build();

        // 🔥 When you want remote host later:
        // .withDockerHost("tcp://REMOTE_IP:2375")

        ApacheDockerHttpClient httpClient =
                new ApacheDockerHttpClient.Builder()
                        .dockerHost(config.getDockerHost())
                        .sslConfig(config.getSSLConfig())
                        .connectionTimeout(Duration.ofSeconds(5))
                        .responseTimeout(Duration.ofSeconds(30))
                        .build();

        return DockerClientImpl.getInstance(config, httpClient);
    }
}

