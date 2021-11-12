function unCapitalizeFirstLetter(string: string): string {
    if (string === undefined) return '';
    return string.charAt(0).toLowerCase() + string.slice(1);
}

function capitalize(string: string): string {
    if (string === undefined) return '';
    return string.charAt(0).toUpperCase() + string.slice(1);
}

export default {
    capitalize,
    unCapitalizeFirstLetter,
}