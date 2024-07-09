export const formatDate = (date) => {
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0'); // Adiciona zero à esquerda se necessário
    const day = String(d.getDate()).padStart(2, '0'); // Adiciona zero à esquerda se necessário
    return `${year}-${month}-${day}`;
};

export const authorization = {
    headers: {
        Authorization: `Bearer ${localStorage.getItem('accessToken')}`
    }
};