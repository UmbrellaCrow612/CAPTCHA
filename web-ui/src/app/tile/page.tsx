"use client"

import { useState, useEffect } from "react"
import Image from "next/image"
import { RefreshCw } from "lucide-react"

export default function CaptchaTile() {
  const [imageUrl, setImageUrl] = useState<string | null>(null)
  const [matrix, setMatrix] = useState<number[][]>([])
  const [captchaId, setCaptchaId] = useState<string>("")
  const [error, setError] = useState<string | null>(null)
  const [isLoading, setIsLoading] = useState(true)

  useEffect(() => {
    fetchCaptcha()
  }, [])

  const fetchCaptcha = async () => {
    setIsLoading(true)
    setError(null)
    try {
      const response = await fetch("https://localhost:7153/captcha/tile")
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`)
      }
      const blob = await response.blob()
      const url = URL.createObjectURL(blob)
      setImageUrl(url)

      const baseMatrixHeader = response.headers.get("X-Base-Matrix")
      console.log("X-Base-Matrix header:", baseMatrixHeader)

      let baseMatrix
      try {
        baseMatrix = JSON.parse(baseMatrixHeader || "[]")
      } catch (e) {
        console.error("Error parsing X-Base-Matrix:", e)
        baseMatrix = []
      }

      console.log("Parsed base matrix:", baseMatrix)

      if (!Array.isArray(baseMatrix) || baseMatrix.length === 0) {
        baseMatrix = Array(5).fill(Array(5).fill(0))
      }

      setMatrix(baseMatrix)
      setCaptchaId(response.headers.get("X-Captcha-Id") || "")
    } catch (e) {
      console.error("Error fetching captcha:", e)
      setError("Failed to fetch captcha. Please try again.")
    } finally {
      setIsLoading(false)
    }
  }

  const handleTileClick = (rowIndex: number, colIndex: number) => {
    const newMatrix = matrix.map((row, i) =>
      row.map((col, j) => (i === rowIndex && j === colIndex ? (col === 0 ? 1 : 0) : col)),
    )
    setMatrix(newMatrix)
  }

  return (
    <div className="container mx-auto p-4 max-w-4xl">
      <h1 className="text-3xl font-bold mb-6 text-center text-gray-800">Captcha Tile</h1>
      {error && (
        <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 mb-6" role="alert">
          <p>{error}</p>
        </div>
      )}
      <div className="bg-white shadow-lg rounded-lg overflow-hidden">
        <div className="flex flex-col md:flex-row">
          <div className="md:w-1/2 p-4 flex items-center justify-center bg-gray-100">
            {isLoading ? (
              <div className="flex items-center justify-center h-[300px]">
                <div className="animate-spin rounded-full h-16 w-16 border-b-2 border-gray-900"></div>
              </div>
            ) : (
              imageUrl && (
                <div className="relative">
                  <Image
                    src={imageUrl || "/placeholder.svg"}
                    alt="Captcha"
                    width={300}
                    height={300}
                    className="w-full h-auto object-contain"
                  />
                  <button
                    onClick={fetchCaptcha}
                    className="absolute top-2 right-2 bg-white p-2 rounded-full shadow-md hover:bg-gray-100 transition-colors duration-200"
                    aria-label="Refresh Captcha"
                  >
                    <RefreshCw className="w-5 h-5 text-gray-600" />
                  </button>
                </div>
              )
            )}
          </div>
          <div className="md:w-1/2 p-4">
            <div className="grid grid-cols-5 gap-2 mb-6">
              {matrix.map((row, rowIndex) =>
                row.map((col, colIndex) => (
                  <button
                    key={`${rowIndex}-${colIndex}`}
                    className={`w-12 h-12 rounded-md transition-all duration-200 ease-in-out transform hover:scale-105 ${
                      col === 1 ? "bg-blue-500 hover:bg-blue-600" : "bg-gray-200 hover:bg-gray-300"
                    }`}
                    onClick={() => handleTileClick(rowIndex, colIndex)}
                  >
                    <span className="sr-only">{col ? "Selected" : "Unselected"}</span>
                  </button>
                )),
              )}
            </div>
            <div>
              <h2 className="text-lg font-semibold mb-2 text-gray-700">Captcha ID:</h2>
              <p className="bg-gray-100 p-3 rounded-md text-sm break-all">{captchaId}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

