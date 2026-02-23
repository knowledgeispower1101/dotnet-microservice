-- =============================================================
-- User Service — Database Initialization Script
-- Runs once on a fresh PostgreSQL volume via
-- /docker-entrypoint-initdb.d/
-- =============================================================

-- 1. Schema
CREATE SCHEMA IF NOT EXISTS user_schema;

-- 2. Extension (pgcrypto gives us gen_random_uuid())
CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA user_schema;

-- =============================================================
-- 3. Tables
-- =============================================================

-- users_app
CREATE TABLE IF NOT EXISTS user_schema.users_app (
    id               UUID        NOT NULL DEFAULT user_schema.gen_random_uuid(),
    email            VARCHAR(255) NOT NULL,
    username         VARCHAR(100) NOT NULL,
    password_hash    VARCHAR(255),
    first_name       VARCHAR(100),
    last_name        VARCHAR(100),
    phone_number     VARCHAR(20),
    is_active        BOOLEAN     NOT NULL DEFAULT TRUE,
    is_email_verified BOOLEAN    NOT NULL DEFAULT FALSE,
    last_login_at    TIMESTAMP WITHOUT TIME ZONE,
    created_at       TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at       TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at       TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT users_app_pkey PRIMARY KEY (id),
    CONSTRAINT users_app_email_key    UNIQUE (email),
    CONSTRAINT users_app_username_key UNIQUE (username)
);

CREATE INDEX IF NOT EXISTS idx_users_email      ON user_schema.users_app (email);
CREATE INDEX IF NOT EXISTS idx_users_username   ON user_schema.users_app (username);
CREATE INDEX IF NOT EXISTS idx_users_is_active  ON user_schema.users_app (is_active);
CREATE INDEX IF NOT EXISTS idx_users_deleted_at ON user_schema.users_app (deleted_at);

-- user_profiles
CREATE TABLE IF NOT EXISTS user_schema.user_profiles (
    id            UUID NOT NULL DEFAULT user_schema.gen_random_uuid(),
    user_id       UUID NOT NULL,
    avatar_url    VARCHAR(500),
    bio           TEXT,
    date_of_birth DATE,
    gender        VARCHAR(20),
    address_line1 VARCHAR(255),
    address_line2 VARCHAR(255),
    city          VARCHAR(100),
    state         VARCHAR(100),
    postal_code   VARCHAR(20),
    country       VARCHAR(100),
    created_at    TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at    TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at    TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT user_profiles_pkey        PRIMARY KEY (id),
    CONSTRAINT user_profiles_user_id_key UNIQUE (user_id),
    CONSTRAINT user_profiles_user_id_fkey
        FOREIGN KEY (user_id) REFERENCES user_schema.users_app (id)
        ON DELETE CASCADE
);

-- roles
CREATE TABLE IF NOT EXISTS user_schema.roles (
    id          UUID         NOT NULL DEFAULT user_schema.gen_random_uuid(),
    role_name   VARCHAR(50)  NOT NULL,
    description TEXT,
    created_at  TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at  TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at  TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT roles_pkey         PRIMARY KEY (id),
    CONSTRAINT roles_role_name_key UNIQUE (role_name)
);

CREATE INDEX IF NOT EXISTS idx_roles_deleted_at ON user_schema.roles (deleted_at);

-- permissions
CREATE TABLE IF NOT EXISTS user_schema.permissions (
    id              UUID         NOT NULL DEFAULT user_schema.gen_random_uuid(),
    permission_name VARCHAR(100) NOT NULL,
    description     TEXT,
    created_at      TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at      TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at      TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT permissions_pkey                PRIMARY KEY (id),
    CONSTRAINT permissions_permission_name_key UNIQUE (permission_name)
);

CREATE INDEX IF NOT EXISTS idx_permissions_deleted_at ON user_schema.permissions (deleted_at);

-- role_permissions (join table)
CREATE TABLE IF NOT EXISTS user_schema.role_permissions (
    role_id       UUID NOT NULL,
    permission_id UUID NOT NULL,
    CONSTRAINT role_permissions_pkey PRIMARY KEY (role_id, permission_id),
    CONSTRAINT role_permissions_role_id_fkey
        FOREIGN KEY (role_id) REFERENCES user_schema.roles (id)
        ON DELETE CASCADE,
    CONSTRAINT role_permissions_permission_id_fkey
        FOREIGN KEY (permission_id) REFERENCES user_schema.permissions (id)
        ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_role_permissions_role_id       ON user_schema.role_permissions (role_id);
CREATE INDEX IF NOT EXISTS idx_role_permissions_permission_id ON user_schema.role_permissions (permission_id);

-- user_roles (join table)
CREATE TABLE IF NOT EXISTS user_schema.user_roles (
    user_id     UUID      NOT NULL,
    role_id     UUID      NOT NULL,
    assigned_at TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT user_roles_pkey PRIMARY KEY (user_id, role_id),
    CONSTRAINT user_roles_user_id_fkey
        FOREIGN KEY (user_id) REFERENCES user_schema.users_app (id)
        ON DELETE CASCADE,
    CONSTRAINT user_roles_role_id_fkey
        FOREIGN KEY (role_id) REFERENCES user_schema.roles (id)
        ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_user_roles_user_id ON user_schema.user_roles (user_id);
CREATE INDEX IF NOT EXISTS idx_user_roles_role_id ON user_schema.user_roles (role_id);

-- =============================================================
-- 4. Seed Data
-- =============================================================

INSERT INTO user_schema.roles (role_name, description)
VALUES
    ('GUEST',  'Default role for newly registered users'),
    ('USER',   'Authenticated user with standard access'),
    ('ADMIN',  'Administrator with full access')
ON CONFLICT (role_name) DO NOTHING;
