(function() {
    Array.prototype.AnyMatch = function(obj, conditions) {
        var found = false,
            i = 0,
            length = this.length;
        //If no condition look for an exact match
        if (!conditions) {
            conditions = function(l, o) {
                return l === o
            };
        }
        for (; i < length; i++) {
            if (!!conditions(this[i], obj)) {
                found = true;
                break;//stop if a match found
            }
        }
        return found;
    }
})();