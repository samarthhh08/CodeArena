package com.cjs.api_gateway.security;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.security.Keys;

import java.security.Key;
import java.util.List;

public class JwtUtil {

    private static final String SECRET =
            "my-super-secret-key-my-super-secret-key"; // move to env

    private static final Key KEY =
            Keys.hmacShaKeyFor(SECRET.getBytes());

    public static Claims validateAndGetClaims(String token) {
        return Jwts.parserBuilder()
                .setSigningKey(KEY)
                .build()
                .parseClaimsJws(token)
                .getBody();
    }

    public static List<String> getRoles(Claims claims) {
        return claims.get("roles", List.class);
    }
}
