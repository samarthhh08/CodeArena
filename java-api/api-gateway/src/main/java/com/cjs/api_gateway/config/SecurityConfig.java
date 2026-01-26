package com.cjs.api_gateway.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.web.reactive.EnableWebFluxSecurity;
import org.springframework.security.config.web.server.ServerHttpSecurity;
import org.springframework.security.web.server.SecurityWebFilterChain;

import com.cjs.api_gateway.security.JwtCookieSecurityContextRepository;

@Configuration
@EnableWebFluxSecurity
public class SecurityConfig {

    @Bean
    public SecurityWebFilterChain securityWebFilterChain(
            ServerHttpSecurity http) {

        return http
                .csrf(ServerHttpSecurity.CsrfSpec::disable)
                .httpBasic(ServerHttpSecurity.HttpBasicSpec::disable)
                .formLogin(ServerHttpSecurity.FormLoginSpec::disable)

                // 🍪 JWT from cookies
                .securityContextRepository(
                        new JwtCookieSecurityContextRepository())

                .authorizeExchange(ex -> ex

                        // Public routes
                        .pathMatchers(HttpMethod.POST,
                                "/api/auth/signin",
                                "/api/auth/signup")
                        .permitAll()

                        // USER
                        .pathMatchers("/api/code/**")
                        .hasRole("USER")

                        .pathMatchers(HttpMethod.GET, "/api/problems/**")
                        .hasAnyRole("USER", "ADMIN")

                        // 👑 Write access (ADMIN only)
                        .pathMatchers(HttpMethod.POST, "/api/problems/**").hasRole("ADMIN")
                        .pathMatchers(HttpMethod.PUT, "/api/problems/**").hasRole("ADMIN")
                        .pathMatchers(HttpMethod.DELETE, "/api/problems/**").hasRole("ADMIN")

                         .pathMatchers("/api/problems/**").permitAll()

                        // ADMIN
                        .pathMatchers("/api/admin/**")
                        .hasRole("ADMIN")

                        // Everything else
                        .anyExchange().authenticated())
                .build();
    }
}
