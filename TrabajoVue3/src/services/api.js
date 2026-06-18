const API_URL = "https://localhost:7052/api";

export async function obtenerPatrimonio() {

    const respuesta = await fetch(`${API_URL}/WalletApi/patrimonio`);

    if (!respuesta.ok) {

        throw new Error("Error al obtener el patrimonio.");

    }

    return await respuesta.json();

}
export async function obtenerCotizacion(codigoCripto) {

    const respuesta = await fetch(`${API_URL}/WalletApi/cotizacion/${codigoCripto}`);

    if (!respuesta.ok) {

        throw new Error("Error al obtener la cotización.");

    }

    return await respuesta.json();

}