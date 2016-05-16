function IllegalStateException(message) {
   this.message = message;
   this.name = "IllegalStateException";
}

var Preconditions = {
  checkState: function(state, message) {
    if (!state) {
      throw new IllegalStateException(message);
    }
  }
}

var Postconditions = {
  checkState: function(state, message) {
    if (!state) {
      throw new IllegalStateException(message);
    }
  }
}
