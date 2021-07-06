export class Error {
    errorMessage: string;

    constructor(errorMessage: string) {
      this.errorMessage = errorMessage;
    }
  
    getErrorMessage(): string {
      return this.errorMessage;
    }
  }