// Email pattern
export const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

// Password pattern: 8-20 characters, at least one uppercase letter, one lowercase letter, one number, and one special character
export const passwordRegex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&*?^()_+\-\[\]{};':"\\|,.<>/~`]).{8,20}$/;

// Name pattern: quadruple name pattern
export const nameRegex = /^\p{L}+\s+\p{L}+\s+\p{L}+\s+\p{L}+$/u;

// Code pattern: 6 digits
export const codeRegex = /^\d{6}$/;

// Functions that check validation using regex patterns
export const validateEmail = (email) => emailRegex.test(email.trim());
export const validatePassword = (password) => passwordRegex.test(password);
export const validateQuadrupleName = (name) => nameRegex.test(name.trim());
export const validateCode = (code) => codeRegex.test(code.trim());