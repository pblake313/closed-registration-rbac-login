<script lang="ts">
    import './ProjectDetails.css'
</script>

<div class="projectDetailsContainer" id="projectstarter">
    <h2 style="text-align: center;">Project Details</h2>
    <p style="text-align: center;"><em>closed-registration-rbac-login</em></p>
    <div style="height: 20px;"></div>

    <h3>Project Summary</h3>
    <p>
        This project is a closed-registration authentication system where only administrators can create new user accounts. 
        It implements full RBAC (Role-Based Access Control) to control exactly which users can access which features and API endpoints. 
        The backend is built with .NET and a T-SQL database, while the frontend uses Svelte for a fast and clean UI.
        <br><br>
        Every protected route in the backend checks both authentication and permission scopes, ensuring users only see and perform 
        actions they are explicitly allowed to. Admins can manage users, assign permissions, and oversee the entire system from a secure admin dashboard.
    </p>
    <br>

    <h3>Purpose</h3>
    <p>
        The purpose of this project is to provide a ready-to-use starter kit for developers who want to jump straight into building features 
        without wasting time on the annoying parts of authentication, access control, and user management. It also serves as a demonstration 
        of my skills in backend architecture, security, and modern frontend development.
    </p>
    <br>

    <h3>Technologies</h3>
    <p>
        <strong>Backend:</strong> .NET 10, C#, Dapper, SQL Server (T-SQL), JWT Authentication, Middleware Pipeline<br>
        <strong>Frontend:</strong> Svelte, TypeScript, Fetch Wrappers, Form Validation<br>
        <strong>Infrastructure:</strong> REST API design, role-based permission enforcement, secure cookie handling
    </p>
    <br>

    <h3>Backend</h3>
    <p>
        The backend uses a clean architecture with repositories, DTOs, and services for handling all major operations. 
        It exposes a set of REST endpoints protected by custom middleware layers, ensuring every request is authenticated 
        and authorized before hitting any controller logic. The SQL Server database stores users, permissions, and login metadata, 
        using Dapper for fast and lightweight data access.
    </p>
    <br>

    <h3>Auth</h3>
    <p>
        Authentication is powered by access tokens and refresh tokens, both validated through custom middleware. 
        Access tokens carry user identity and permissions, while refresh tokens allow seamless re-authentication without 
        constantly logging in again. All protected endpoints validate permissions server-side, preventing unauthorized use 
        even if someone manipulates the frontend.
    </p>
    <br>

    <h3>Frontend</h3>
    <p>
        The frontend is built with Svelte and TypeScript, providing a fast, reactive interface for both users and admins. 
        It includes login flows, permission-aware UI rendering, dashboards, and reusable components for form handling and 
        navigation. Fetch wrappers automatically attach tokens, handle auto-login, and trigger forced logout when needed, 
        keeping the experience smooth and secure.
    </p>
        
</div>
<div style="height: 100px;"></div>