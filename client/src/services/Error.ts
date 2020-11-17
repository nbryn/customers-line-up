export class Error {
    errors: any;

    constructor(errors: any) {
      this.errors = errors;
    }
  
    getErrors() {
      return this.errors;
    }
  }