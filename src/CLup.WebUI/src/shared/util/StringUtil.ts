function capitalize(string: string): string {
    if (string === undefined) return '';
    return string.charAt(0).toUpperCase() + string.slice(1);
}

export default {
    capitalize,
};
