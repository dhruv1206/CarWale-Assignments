### Assignment on Git, GitHub, and Web Fundamentals

---

#### Section 1: Git and GitHub

**1. Difference between Git and GitHub**  
Git and GitHub are often mentioned together, but they serve distinct purposes:  
- **Git**: A distributed version control system used for tracking changes in code and managing software projects. It allows multiple developers to collaborate and work on a codebase simultaneously.
- **GitHub**: A cloud-based platform that hosts Git repositories. It provides a user-friendly interface for managing Git repositories, collaboration tools, and additional features like issue tracking and CI/CD integration.

**2. GitHub Features**  
GitHub provides several features that enhance the version control process, including:
- **Fork**: Creating a copy of someone else's repository to work on independently.
- **Pull Request (PR)**: Requesting that your changes be merged into someone else's branch/repository.
- **Merge**: Integrating changes from one branch into another.

**3. Key Git Commands**  
Here are some commonly used Git commands:
1. `git init`: Initializes a new Git repository.
2. `git add <filename>`: Stages changes made to a specific file.
3. `git commit -m "message"`: Commits changes with a descriptive message.
4. `git push origin <branch-name>`: Pushes committed changes to a specific branch on the remote repository.
5. `git pull origin <branch-name>`: Pulls the latest changes from a specific branch on the remote repository.
6. `git fetch origin <branch-name>`: Fetches all the commits and history from a particular branch.
7. `git merge origin/<branch-name>`: Merges changes from one branch into the current branch using fetched data.

**Note**: `git pull` is a combination of `git fetch` and `git merge`.

8. `git status`: Displays the status of files in the repository (e.g., staged, modified, untracked).
9. `git config --global user.name "Your Name"`: Sets the username for Git commits.
10. `git diff`: Shows the differences between the working directory and the staging area.
11. `git reset`: Unstages files.
12. `git reset --hard`: Discards local changes permanently.
13. `git rm <filename>`: Removes a file from the repository.
14. `git log`: Displays the commit history.
15. `git branch`: Lists all branches in the repository.
16. `git branch <branch-name>`: Creates a new branch.
17. `git checkout <branch-name>`: Switches to another branch.
18. `git stash`: Temporarily stores changes and resets the working directory to the latest commit.

**4. Git Merge vs. Rebase vs. Squash Commit**
- **Git Merge**: Merges changes from one branch into another, keeping the history of both branches intact.
- **Git Rebase**: Re-applies commits from the feature branch on top of another branch (usually `main`), creating a linear history.
- **Git Squash**: Combines multiple commits into a single commit before merging it into another branch.

---

#### Section 2: Web Fundamentals

**1. Introduction to HTTP and HTTPS**  
- **HTTP (Hypertext Transfer Protocol)** is the protocol used for transferring web pages and data between clients (browsers) and servers. It defines how requests and responses should be formatted and exchanged.
- **HTTPS (Hypertext Transfer Protocol Secure)** is an extension of HTTP that adds a layer of security through SSL/TLS encryption, ensuring that the data transmitted is protected from tampering.

**2. HTTP Headers**  
Headers are metadata used in HTTP requests and responses, providing information about the communication between the client and the server.  
Common types of HTTP headers:
- **Request Headers**: Information sent by the client to the server (e.g., user-agent, cookies).
- **Response Headers**: Information sent by the server to the client (e.g., content-type, location).
- **Representation Headers**: Details about the representation of the resource (e.g., content-length).
- **Payload Headers**: Information about the message body (e.g., content-type of the body).

**3. HTTP Request Methods**  
Different HTTP methods define the actions a client can request from the server:
1. **GET**: Retrieves data from the server.
2. **POST**: Sends data to the server (e.g., form submission or file upload).
3. **DELETE**: Deletes data from the server.
4. **PUT**: Replaces data on the server.
5. **PATCH**: Partially updates data on the server.
6. **HEAD**: Retrieves only metadata without the body of the response.
7. **OPTIONS**: Returns the allowed methods and operations for a given resource.
8. **TRACE**: Used to diagnose the path a request takes to the server.
9. **CONNECT**: Establishes a tunnel to the server, commonly used with proxy servers.

**4. HTTP Response Status Codes**  
Response status codes indicate the outcome of an HTTP request. They are divided into categories:
- **1xx**: Informational responses (e.g., `100 Continue`).
- **2xx**: Successful responses (e.g., `200 OK`, `201 Created`).
- **3xx**: Redirection responses (e.g., `301 Moved Permanently`).
- **4xx**: Client errors (e.g., `400 Bad Request`, `404 Not Found`).
- **5xx**: Server errors (e.g., `500 Internal Server Error`).

**Common Status Codes:**
- **200**: OK – The request was successful.
- **201**: Created – The request was successful and a new resource was created.
- **400**: Bad Request – The request was invalid or malformed.
- **401**: Unauthorized – Authentication is required and has failed or has not yet been provided.
- **404**: Not Found – The resource could not be found.
- **500**: Internal Server Error – The server encountered an unexpected condition.

**5. API Overview**  
An **API (Application Programming Interface)** allows different software systems to communicate with each other. It defines a set of rules for requesting and exchanging data.

**Types of APIs:**
- **Internal API**: Used within an organization, hidden from external users.
- **Open API**: Publicly accessible APIs.
- **Partner API**: Shared between partners with specific access permissions.
- **Composite API**: Combines multiple API calls into a single request to fetch data from different sources.

**6. API Architecture Styles**  
Different styles are used to design APIs, with varying degrees of complexity and flexibility:
- **REST (Representational State Transfer)**: Lightweight, uses HTTP, and typically returns data in JSON or XML format.
- **SOAP (Simple Object Access Protocol)**: A more complex protocol that uses XML over HTTP.
- **GraphQL**: A query language for APIs, enabling clients to request only the data they need from a single endpoint.
- **gRPC (Google Remote Procedure Call)**: A high-performance framework for distributed systems using HTTP/2 and Protocol Buffers.
- **WebSockets**: Enables two-way communication between the server and client, often used for real-time applications.
- **Webhooks**: Allow asynchronous communication where the server sends data to a client when an event occurs.

**7. Best Practices for REST API Design**
To design effective REST APIs, consider the following best practices:
- Use **nouns** rather than verbs when naming resources (e.g., `/users`, not `/getUsers`).
- **Version** the API for backward compatibility (e.g., `/api/v1/`).
- Use appropriate **HTTP status codes** to indicate the outcome of requests.
- Implement **pagination** and **filtering** for large datasets to improve performance and user experience.

---
