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
export async function comprar(transaccion) {

    const respuesta = await fetch(`${API_URL}/WalletApi/comprar`, {

        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(transaccion)

    })

    if (!respuesta.ok) {

        throw new Error("No se pudo registrar la compra.")

    }
}
export async function vender(transaccion) {

    const respuesta = await fetch(`${API_URL}/WalletApi/vender`, {

        method: "POST",

        headers: {

            "Content-Type": "application/json"

        },

        body: JSON.stringify(transaccion)

    })

    if (!respuesta.ok) {

        throw new Error("Error al realizar la venta.")

    }

}
export async function obtenerSaldo(codigoCripto) {

    const respuesta = await fetch(`${API_URL}/WalletApi/saldo/${codigoCripto}`);

    if (!respuesta.ok) {

        throw new Error("Error al obtener el saldo.");

    }

    return await respuesta.json();

}
export async function obtenerPortfolio() {

    const respuesta = await fetch(`${API_URL}/WalletApi/portfolio`)

    if (!respuesta.ok) {

        throw new Error("Error al obtener el portfolio.")

    }

    return await respuesta.json()

}